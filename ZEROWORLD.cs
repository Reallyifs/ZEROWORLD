using System.Reflection;
using Terraria;
using Terraria.ModLoader;
using ZEROWORLD.Files;
using ZEROWORLD.Items;

namespace ZEROWORLD
{
	public class ZEROWORLD : Mod
	{
		public static bool DeveloperMode
		{
			get;
		} = true;

		internal static Assembly Assembly
		{
			get;
		} = Assembly.GetExecutingAssembly();

		internal static ZEROWORLD Instance
		{
			get;
			private set;
		}

		public ZEROWORLD()
		{
			Instance = this;
			ZAction.Initialize();
		}

		public override void Load()
		{
			ZAction.Load();
			Main.OnTick += ZAction.TickDraw;
		}

		public override void Unload()
		{
			ZAction.Unload();
			Main.OnTick -= ZAction.TickDraw;
		}

		#region 引用其他类的方法
		public override void AddRecipeGroups() => ZRecipes.RecipeGroups();
		public override void AddRecipes() => ZRecipes.Recipes();
		public override object Call(params object[] args) => ZCall.Call(args);
		#endregion
	}
}