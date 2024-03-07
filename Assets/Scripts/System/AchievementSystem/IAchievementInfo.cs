using System;
using UnityEngine;

namespace daifuDemo
{
    public interface IAchievementInfo
    {
        string Name { get; }
        
        Sprite Icon { get; }
        
        string Description { get; }
        
        Func<bool> IfComplete { get; }
        
        Action Award { get; }

        IAchievementInfo WithName(string name);

        IAchievementInfo WithIcon(Sprite icon);

        IAchievementInfo WithDescription(string description);

        IAchievementInfo WithIfComplete(Func<bool> func);

        IAchievementInfo WithAward(Action award);
    }

    public class AchievementInfo : IAchievementInfo
    {
        public string Name { get; private set; }
        
        public Sprite Icon { get; private set; }
        
        public string Description { get; private set; }
        
        public Func<bool> IfComplete { get; private set; }
        
        public Action Award { get; private set; }
        
        public IAchievementInfo WithName(string name)
        {
            Name = name;
            return this;
        }

        public IAchievementInfo WithIcon(Sprite icon)
        {
            Icon = icon;
            return this;
        }

        public IAchievementInfo WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public IAchievementInfo WithIfComplete(Func<bool> ifComplete)
        {
            IfComplete = ifComplete;
            return this;
        }

        public IAchievementInfo WithAward(Action award)
        {
            Award = award;
            return this;
        }
    }
}