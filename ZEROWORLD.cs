using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
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
			ZAction.LoadAction();
			Main.OnTick += ZAction.TickDraw;
		}

		public override void Unload()
		{
			ZAction.UnloadAction();
			Main.OnTick -= ZAction.TickDraw;
		}

		public override void PostDrawInterface(SpriteBatch spriteBatch)
		{
			ZAction.PostDrawAction(spriteBatch);
		}

		#region 引用其他类的方法
		public override void AddRecipeGroups() => ZRecipes.RecipeGroups();
		public override void AddRecipes() => ZRecipes.Recipes();
		public override object Call(params object[] args) => ZCall.Call(args);
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) => ZDraw.ModifyInterfaceLayers(layers);
		#endregion
	}
}