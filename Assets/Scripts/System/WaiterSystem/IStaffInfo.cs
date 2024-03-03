using System.Collections.Generic;

namespace daifuDemo
{
    public interface IStaffInfo
    {
        string Key { get; }
        
        string Name { get; }
        
        List<(int, float)> RankWithWalkSpeed { get; }
        
        List<(int, float)> RankWithCookSpeed { get; }

        IStaffInfo WithKey(string key);

        IStaffInfo WithName(string name);

        IStaffInfo WithRankWithWalkSpeed(List<(int, float)> rankWithWalkSpeed);

        IStaffInfo WithRankWithCookSpeed(List<(int, float)> rankWithCookSpeed);
    }

    public class StaffInfo : IStaffInfo
    {
        public string Key { get; private set; }
        
        public string Name { get; private set; }
        
        public List<(int, float)> RankWithWalkSpeed { get; private set; }
        
        public List<(int, float)> RankWithCookSpeed { get; private set; }

        public IStaffInfo WithKey(string key)
        {
            Key = key;
            return this;
        }

        public IStaffInfo WithName(string name)
        {
            Name = name;
            return this;
        }

        public IStaffInfo WithRankWithWalkSpeed(List<(int, float)> rankWithWalkSpeed)
        {
            RankWithWalkSpeed = rankWithWalkSpeed;
            return this;
        }

        public IStaffInfo WithRankWithCookSpeed(List<(int, float)> rankWithCookSpeed)
        {
            RankWithCookSpeed = rankWithCookSpeed;
            return this;
        }
    }
}