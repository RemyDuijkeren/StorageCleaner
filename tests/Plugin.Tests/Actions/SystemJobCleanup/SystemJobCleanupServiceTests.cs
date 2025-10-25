using System.Xml.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using StorageCleaner.Actions.SystemJobCleanup;

namespace Plugin.Tests.Actions.SystemJobCleanup;

public class SystemJobCleanupServiceTests
{
    private static IOrganizationService CreateServiceReturning(Entity entity)
    {
        var service = Substitute.For<IOrganizationService>();
        service.Retrieve("organization", entity.Id, Arg.Any<ColumnSet>()).Returns(entity);
        return service;
    }

    [Fact]
    public void Load_ReturnsDefaults_When_OrgSettingsMissing()
    {
        // Arrange
        var orgId = Guid.NewGuid();
        var organization = new Entity("organization") { Id = orgId }; // no orgdborgsettings
        var service = CreateServiceReturning(organization);
        var sut = new SystemJobCleanupService();

        // Act
        var result = sut.Load(service, orgId);

        // Assert
        result.EnableSystemJobCleanup.ShouldBeTrue();
        result.SucceededSystemJobPersistenceInDays.ShouldBe(30);
        result.CanceledSystemJobPersistenceInDays.ShouldBe(60);
        result.FailedSystemJobPersistenceInDays.ShouldBe(60);
    }

    [Fact]
    public void Load_ParsesValues_When_ValidXmlPresent()
    {
        // Arrange
        var xml = new XDocument(
            new XElement("OrgSettings",
                new XElement("EnableSystemJobCleanup", "false"),
                new XElement("SucceededSystemJobPersistenceInDays", "5"),
                new XElement("CanceledSystemJobPersistenceInDays", "10"),
                new XElement("FailedSystemJobPersistenceInDays", "15")
            )).ToString(SaveOptions.DisableFormatting);

        var orgId = Guid.NewGuid();
        var organization = new Entity("organization") { Id = orgId };
        organization["orgdborgsettings"] = xml;
        var service = CreateServiceReturning(organization);
        var sut = new SystemJobCleanupService();

        // Act
        var result = sut.Load(service, orgId);

        // Assert
        result.EnableSystemJobCleanup.ShouldBeFalse();
        result.SucceededSystemJobPersistenceInDays.ShouldBe(5);
        result.CanceledSystemJobPersistenceInDays.ShouldBe(10);
        result.FailedSystemJobPersistenceInDays.ShouldBe(15);
    }

    [Fact]
    public void Load_ClampsOutOfRangeValues_When_ValuesExceedLimits()
    {
        // Arrange
        var xml = new XDocument(
            new XElement("OrgSettings",
                new XElement("EnableSystemJobCleanup", "true"),
                new XElement("SucceededSystemJobPersistenceInDays", "999"),
                new XElement("CanceledSystemJobPersistenceInDays", "-1"),
                new XElement("FailedSystemJobPersistenceInDays", "181")
            )).ToString(SaveOptions.DisableFormatting);

        var orgId = Guid.NewGuid();
        var organization = new Entity("organization") { Id = orgId };
        organization["orgdborgsettings"] = xml;
        var service = CreateServiceReturning(organization);
        var sut = new SystemJobCleanupService();

        // Act
        var result = sut.Load(service, orgId);

        // Assert
        result.SucceededSystemJobPersistenceInDays.ShouldBe(90); // clamped to max
        result.CanceledSystemJobPersistenceInDays.ShouldBe(0);   // clamped to min
        result.FailedSystemJobPersistenceInDays.ShouldBe(180);   // clamped to max
    }

    [Fact]
    public void Save_ReturnsErrorAndDoesNotUpdate_When_FeatureInvalid()
    {
        // Arrange
        var orgId = Guid.NewGuid();
        var organization = new Entity("organization") { Id = orgId };
        var service = CreateServiceReturning(organization);
        var sut = new SystemJobCleanupService();
        var feature = new SystemJobCleanupFeature
        {
            EnableSystemJobCleanup = true,
            SucceededSystemJobPersistenceInDays = 200, // invalid (>90)
            CanceledSystemJobPersistenceInDays = 10,
            FailedSystemJobPersistenceInDays = 10
        };

        // Act
        var (ok, error) = sut.Save(service, orgId, feature);

        // Assert
        ok.ShouldBeFalse();
        error.ShouldNotBeNull();
        service.DidNotReceive().Update(Arg.Any<Entity>());
    }

    [Fact]
    public void Save_MergesAndPreservesUnrelatedSettings_When_UpdatingExistingXml()
    {
        // Arrange
        var existing = new XDocument(
            new XElement("OrgSettings",
                new XElement("SomeOther", "abc"),
                new XElement("EnableSystemJobCleanup", "false"),
                new XElement("SucceededSystemJobPersistenceInDays", "1"),
                new XElement("CanceledSystemJobPersistenceInDays", "2"),
                new XElement("FailedSystemJobPersistenceInDays", "3")
            )).ToString(SaveOptions.DisableFormatting);

        var orgId = Guid.NewGuid();
        var organization = new Entity("organization") { Id = orgId };
        organization["orgdborgsettings"] = existing;
        var service = CreateServiceReturning(organization);
        var sut = new SystemJobCleanupService();
        var feature = new SystemJobCleanupFeature
        {
            EnableSystemJobCleanup = true,
            SucceededSystemJobPersistenceInDays = 10,
            CanceledSystemJobPersistenceInDays = 20,
            FailedSystemJobPersistenceInDays = 30
        };

        // capture update argument
        Entity? updated = null;
        service.When(s => s.Update(Arg.Any<Entity>())).Do(ci => updated = (Entity)ci[0]);

        // Act
        var (ok, error) = sut.Save(service, orgId, feature);

        // Assert
        ok.ShouldBeTrue();
        error.ShouldBeNull();
        service.Received(1).Update(Arg.Any<Entity>());
        updated.ShouldNotBeNull();
        var xml = updated!.GetAttributeValue<string>("orgdborgsettings");
        xml.ShouldNotBeNullOrWhiteSpace();
        var xdoc = XDocument.Parse(xml);
        var root = xdoc.Root!;

        // unrelated preserved
        root.Elements().Any(el => el.Name.LocalName == "SomeOther" && el.Value == "abc").ShouldBeTrue();

        // updated values
        root.Elements().First(e2 => e2.Name.LocalName == "EnableSystemJobCleanup").Value.ShouldBe("true");
        root.Elements().First(e2 => e2.Name.LocalName == "SucceededSystemJobPersistenceInDays").Value.ShouldBe("10");
        root.Elements().First(e2 => e2.Name.LocalName == "CanceledSystemJobPersistenceInDays").Value.ShouldBe("20");
        root.Elements().First(e2 => e2.Name.LocalName == "FailedSystemJobPersistenceInDays").Value.ShouldBe("30");
    }

    [Fact]
    public void Save_CreatesXml_When_OrgSettingsEmptyOrInvalid()
    {
        // Arrange
        var orgId = Guid.NewGuid();
        var organization = new Entity("organization") { Id = orgId };
        organization["orgdborgsettings"] = null; // empty/invalid
        var service = CreateServiceReturning(organization);
        var sut = new SystemJobCleanupService();
        var feature = new SystemJobCleanupFeature
        {
            EnableSystemJobCleanup = false,
            SucceededSystemJobPersistenceInDays = 1,
            CanceledSystemJobPersistenceInDays = 2,
            FailedSystemJobPersistenceInDays = 3
        };

        // capture update argument
        Entity? updated = null;
        service.When(s => s.Update(Arg.Any<Entity>())).Do(ci => updated = (Entity)ci[0]);

        // Act
        var (ok, error) = sut.Save(service, orgId, feature);

        // Assert
        ok.ShouldBeTrue();
        error.ShouldBeNull();
        service.Received().Update(Arg.Any<Entity>());
        updated.ShouldNotBeNull();
        var xml = updated!.GetAttributeValue<string>("orgdborgsettings");
        var xdoc = XDocument.Parse(xml);
        xdoc.Root!.Name.LocalName.ShouldBe("OrgSettings");
        xdoc.Root!.Elements().Count().ShouldBeGreaterThanOrEqualTo(4);
    }

    [Fact]
    public void Save_WritesLowercaseBoolean_For_EnableFlag()
    {
        // Arrange
        var orgId = Guid.NewGuid();
        var organization = new Entity("organization") { Id = orgId };
        var service = CreateServiceReturning(organization);
        var sut = new SystemJobCleanupService();
        var feature = new SystemJobCleanupFeature { EnableSystemJobCleanup = true };

        // capture update argument
        Entity? updated = null;
        service.When(s => s.Update(Arg.Any<Entity>())).Do(ci => updated = (Entity)ci[0]);

        // Act
        var (ok, _) = sut.Save(service, orgId, feature);

        // Assert
        ok.ShouldBeTrue();
        service.Received().Update(Arg.Any<Entity>());
        updated.ShouldNotBeNull();
        var xml = updated!.GetAttributeValue<string>("orgdborgsettings");
        var xdoc = XDocument.Parse(xml);
        var value = xdoc.Root!.Elements().First(el => el.Name.LocalName == "EnableSystemJobCleanup").Value;
        value.ShouldBe("true"); // lower case
    }
}
