using System.Reflection;
using Terraria.ModLoader;
using ZEROWORLD.Files;
using ZEROWORLD.Items;

namespace ZEROWORLD
{
	public class ZEROWORLD : Mod
	{
		internal static bool DeveloperMode { get; private set; }
		internal static Assembly Assembly { get; set; }
		internal static ZEROWORLD Instance { get; set; }

		public ZEROWORLD()
		{
			Assembly = Assembly.GetExecutingAssembly();
			Instance = this;
			DeveloperMode = false;
		}

		public override void Load()
		{

		}

		#region 引用其他类的方法
		public override void AddRecipeGroups() => ZRecipes.RecipeGroups();
		public override void AddRecipes() => ZRecipes.Recipes();
		public override object Call(params object[] args) => ZCall.Call(args);
		#endregion
	}
}