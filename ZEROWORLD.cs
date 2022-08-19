using System;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;
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

		public static UnifiedRandom SafeRandom => Main.rand ?? (Main.rand = new UnifiedRandom(20201027));

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

		public override void Load() => ZAction.LoadAction.TryAction(ThrowError);

		public override void Unload()
		{
			ZAction.UnloadAction.TryAction(ThrowError);
			GC.Collect();
		}

		internal static void ThrowError(Exception exception)
		{
			string writeText = $"Message: {exception.Message}\nStackTrace: {exception.StackTrace}";
			ZDeveloperSetting.Write(ZDeveloperSetting.MessageType.Error, "{0}", writeText);
			throw new Exception(writeText);
		}

		#region 引用其他类的方法
		public override void AddRecipeGroups() => ZRecipes.RecipeGroups();
		public override void AddRecipes() => ZRecipes.Recipes();
		public override object Call(params object[] args) => ZCall.Call(args);
		#endregion
	}
}