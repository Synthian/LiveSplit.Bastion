﻿using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.Reflection;
namespace LiveSplit.Bastion {
    public class BastionFactory : IComponentFactory {
        public string ComponentName { get { return "Bastion Autosplitter v" + this.Version.ToString(); } }
        public string Description { get { return "Autosplitter for Bastion"; } }
        public ComponentCategory Category { get { return ComponentCategory.Control; } }
        public IComponent Create(LiveSplitState state) { return new BastionComponent(); }
        public string UpdateName { get { return this.ComponentName; } }
		public string UpdateURL { get { return null; } }
		public string XMLURL { get { return this.UpdateURL; } }
		public Version Version { get { return Assembly.GetExecutingAssembly().GetName().Version; } }
    }
}