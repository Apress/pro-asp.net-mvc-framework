using System.Collections.Generic;

namespace GridDemo.Models
{
    public class MountainInfo
    {
        public string Name { get; set; }
        public int HeightInMeters { get; set; }
    }

    public static class SampleData
    {
        public static readonly List<MountainInfo> SevenSummits = new List<MountainInfo> {
            new MountainInfo { Name = "Everest", HeightInMeters = 8848 },
            new MountainInfo { Name = "Aconcagua", HeightInMeters = 6962 },
            new MountainInfo { Name = "Mount McKinley", HeightInMeters = 6194 },
            new MountainInfo { Name = "Kilimanjaro", HeightInMeters = 5895 },
            new MountainInfo { Name = "Elbrus", HeightInMeters = 5642 },
            new MountainInfo { Name = "Vinson Massif", HeightInMeters = 4892 },
            new MountainInfo { Name = "Carstensz Pyramid", HeightInMeters = 4884 }
        };
    }
}