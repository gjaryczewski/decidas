using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using NuGet.Common;

namespace Decidas.Data;

public static class DevelopmentInitialData
{
    public static void Publish(MainDbContext db)
    {
        var keepers = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };
        var members = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
        var groups = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };
        var topics = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
        
        CleanupData(db);
        PublishAccounts(db, keepers, members);
        PublishGroups(db, groups);
        PublishKeepers(db, groups, keepers);
        PublishMembers(db, groups, members);
        PublishTopics(db, topics);
        PublishGatherings(db, groups, topics);
    }

    private static void CleanupData(MainDbContext db)
    {
        db.Gatherings.RemoveRange(db.Gatherings);
        db.Topics.RemoveRange(db.Topics);
        db.Groups.RemoveRange(db.Groups);
        db.Keepers.RemoveRange(db.Keepers);
        db.Members.RemoveRange(db.Members);
        db.Accounts.RemoveRange(db.Accounts);
        db.SaveChanges();
    }

    private static void PublishAccounts(MainDbContext db, Guid[] keepers, Guid[] members)
    {
        db.Accounts.Add(new Models.Account
        {
            Id = keepers[0],
            Login = "first.keeper",
            Email = "first.keeper@decidas",
            Name = "First Keeper",
            Password = "123",
            RegisterTime = DateTime.Now
        });

        db.Accounts.Add(new Models.Account
        {
            Id = keepers[1],
            Login = "second.keeper",
            Email = "second.keeper@decidas",
            Name = "Second Keeper",
            Password = "123",
            RegisterTime = DateTime.Now
        });

        db.Accounts.Add(new Models.Account
        {
            Id = members[0],
            Login = "first.member",
            Email = "first.member@decidas",
            Name = "First Member",
            Password = "123",
            RegisterTime = DateTime.Now
        });

        db.Accounts.Add(new Models.Account
        {
            Id = members[1],
            Login = "second.member",
            Email = "second.member@decidas",
            Name = "Second Member",
            Password = "123",
            RegisterTime = DateTime.Now
        });

        db.Accounts.Add(new Models.Account
        {
            Id = members[2],
            Login = "third.member",
            Email = "third.member@decidas",
            Name = "Third Member",
            Password = "123",
            RegisterTime = DateTime.Now
        });

        db.SaveChanges();
    }

    private static void PublishGroups(MainDbContext db, Guid[] groups)
    {
        db.Groups.Add(new Models.Group
        {
            Id = groups[0],
            Name = "DG1",
            City = "Some City",
            StartDate = new DateOnly(2024, 03, 20),
            SocialLink = "https://example.com",
            IsActive = true
        });

        db.Groups.Add(new Models.Group
        {
            Id = groups[1],
            Name = "DG2",
            City = "Another City",
            StartDate = new DateOnly(2024, 04, 20),
            SocialLink = "https://example.com",
            IsActive = true
        });

        db.SaveChanges();
    }

    private static void PublishKeepers(MainDbContext db, Guid[] groups, Guid[] keepers)
    {
        db.Keepers.Add(new Models.Keeper
        {
            Id = Guid.NewGuid(),
            Group = db.Groups.First(g => g.Id == groups[0]),
            Account = db.Accounts.First(a => a.Id == keepers[0])
        });

        db.Keepers.Add(new Models.Keeper
        {
            Id = Guid.NewGuid(),
            Group = db.Groups.First(g => g.Id == groups[1]),
            Account = db.Accounts.First(a => a.Id == keepers[0])
        });

        db.Keepers.Add(new Models.Keeper
        {
            Id = Guid.NewGuid(),
            Group = db.Groups.First(g => g.Id == groups[1]),
            Account = db.Accounts.First(a => a.Id == keepers[1])
        });

        db.SaveChanges();
    }

    private static void PublishMembers(MainDbContext db, Guid[] groups, Guid[] members)
    {
        db.Members.Add(new Models.Member
        {
            Id = Guid.NewGuid(),
            Group = db.Groups.First(g => g.Id == groups[0]),
            Account = db.Accounts.First(a => a.Id == members[0])
        });

        db.Members.Add(new Models.Member
        {
            Id = Guid.NewGuid(),
            Group = db.Groups.First(g => g.Id == groups[1]),
            Account = db.Accounts.First(a => a.Id == members[1])
        });

        db.Members.Add(new Models.Member
        {
            Id = Guid.NewGuid(),
            Group = db.Groups.First(g => g.Id == groups[1]),
            Account = db.Accounts.First(a => a.Id == members[2])
        });

        db.SaveChanges();
    }

    private static void PublishTopics(MainDbContext db, Guid[] topics)
    {
        db.Topics.Add(new Models.Topic
        {
            Id = topics[0],
            Title = "Topic 1",
            Order = 1
        });

        db.Topics.Add(new Models.Topic
        {
            Id = topics[1],
            Title = "Topic 2",
            Order = 2
        });

        db.Topics.Add(new Models.Topic
        {
            Id = topics[2],
            Title = "Topic 3",
            Order = 3
        });

        db.Topics.Add(new Models.Topic
        {
            Id = topics[3],
            Title = "Topic 4",
            Order = 4
        });

        db.SaveChanges();
    }

    private static void PublishGatherings(MainDbContext db, Guid[] groups, Guid[] topics)
    {
        db.Gatherings.Add(new Models.Gathering
        {
            Id = Guid.NewGuid(),
            Group = db.Groups.First(g => g.Id == groups[0]),
            Topic = db.Topics.First(g => g.Id == topics[0]),
            MapLink = "https://example.com/maps",
            Time = new DateTime(2024, 05, 10)
        });

        db.Gatherings.Add(new Models.Gathering
        {
            Id = Guid.NewGuid(),
            Group = db.Groups.First(g => g.Id == groups[0]),
            Topic = db.Topics.First(g => g.Id == topics[1]),
            MapLink = "https://example.com/maps",
            Time = new DateTime(2024, 05, 11)
        });

        db.Gatherings.Add(new Models.Gathering
        {
            Id = Guid.NewGuid(),
            Group = db.Groups.First(g => g.Id == groups[0]),
            Topic = db.Topics.First(g => g.Id == topics[2]),
            MapLink = "https://example.com/maps",
            Time = new DateTime(2024, 05, 12)
        });

        db.Gatherings.Add(new Models.Gathering
        {
            Id = Guid.NewGuid(),
            Group = db.Groups.First(g => g.Id == groups[0]),
            Topic = db.Topics.First(g => g.Id == topics[3]),
            MapLink = "https://example.com/maps",
            Time = new DateTime(2024, 05, 13)
        });

        db.Gatherings.Add(new Models.Gathering
        {
            Id = Guid.NewGuid(),
            Group = db.Groups.First(g => g.Id == groups[1]),
            Topic = db.Topics.First(g => g.Id == topics[0]),
            MapLink = "https://example.com/maps",
            Time = new DateTime(2024, 06, 10)
        });

        db.Gatherings.Add(new Models.Gathering
        {
            Id = Guid.NewGuid(),
            Group = db.Groups.First(g => g.Id == groups[1]),
            Topic = db.Topics.First(g => g.Id == topics[1]),
            MapLink = "https://example.com/maps",
            Time = new DateTime(2024, 06, 11)
        });

        db.Gatherings.Add(new Models.Gathering
        {
            Id = Guid.NewGuid(),
            Group = db.Groups.First(g => g.Id == groups[1]),
            Topic = db.Topics.First(g => g.Id == topics[2]),
            MapLink = "https://example.com/maps",
            Time = new DateTime(2024, 06, 12)
        });

        db.Gatherings.Add(new Models.Gathering
        {
            Id = Guid.NewGuid(),
            Group = db.Groups.First(g => g.Id == groups[1]),
            Topic = db.Topics.First(g => g.Id == topics[3]),
            MapLink = "https://example.com/maps",
            Time = new DateTime(2024, 06, 13)
        });

        db.SaveChanges();
    }
}
