using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Locations;
using StardewValley.TerrainFeatures;

namespace ResetTerrainFeatures
{
	// Token: 0x02000005 RID: 5
	public static class Regenerator
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020B9 File Offset: 0x000002B9
		public static void Reload(GameLocation location, Type[] types = null, int[] indices = null) 
		{
			Regenerator.Clear(location, types, indices);
			Regenerator.LoadMapFeatures(location, types, indices);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002258 File Offset: 0x00000458
		public static int[] GetIndicesFromOptions() 
		{
			List<int> list = new();
			List<string> list2 = Regenerator.regeneratorOptions.Keys.ToList();
			foreach (string item in list2)
			{
				bool flag = !Regenerator.regeneratorOptions[item] || !Regenerator.indexKeys.ContainsKey(item);
				if (!flag)
				{
					foreach (int item2 in Regenerator.indexKeys[item])
					{
						list.Add(item2);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000233C File Offset: 0x0000053C
		public static Type[] GetTypesFromOptions() 
		{
			List<Type> list = new();
			List<string> list2 = Regenerator.regeneratorOptions.Keys.ToList();
			for (int i = 0; i < list2.Count; i++)
			{
				string key = list2[i];
				bool flag = Regenerator.regeneratorOptions[key] && Regenerator.typeKeys.ContainsKey(key);
				if (flag)
				{
					list.Add(Regenerator.typeKeys[key]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023C4 File Offset: 0x000005C4
		public static void Clear(GameLocation location, Type[] types = null, int[] parentSheetIndices = null)
		{
			List<Vector2> list = new();
			List<Vector2> list2 = new();
			List<LargeTerrainFeature> list3 = new();
			foreach (Vector2 key in location.terrainFeatures.Keys)
			{
				TerrainFeature terrainFeature = location.terrainFeatures[key];
				bool flag = Regenerator.regeneratorOptions.ContainsKey("Crop") && Regenerator.regeneratorOptions["Crop"] && terrainFeature is HoeDirt;
				if (flag)
				{
					(terrainFeature as HoeDirt).crop = null;
				}
				bool flag2 = types == null || types.Contains(terrainFeature.GetType()) || types.Contains(typeof(TerrainFeature));
				if (flag2)
				{
					list.Add(key);
				}
			}
			foreach (Vector2 key2 in location.objects.Keys)
			{
				StardewValley.Object @object = location.objects[key2];
				bool flag3 = types == null || types.Contains(@object.GetType()) || (parentSheetIndices != null && parentSheetIndices.Contains(@object.ParentSheetIndex)) || types.Contains(typeof(StardewValley.Object)) || (Regenerator.regeneratorOptions.ContainsKey("Forage") && Regenerator.regeneratorOptions["Forage"] && @object.isForage());
				if (flag3)
				{
					list2.Add(key2);
				}
			}
			foreach (LargeTerrainFeature current3 in location.largeTerrainFeatures)
			{
				bool flag4 = types == null || types.Contains(current3.GetType());
				if (flag4)
				{
					list3.Add(current3);
				}
			}
			bool flag5 = location is Farm;
			if (flag5)
			{
				Farm farm = location as Farm;
				List<ResourceClump> list4 = new();
				foreach (ResourceClump current4 in farm.resourceClumps)
				{
					bool flag6 = types == null || types.Contains(current4.GetType()) || (parentSheetIndices != null && parentSheetIndices.Contains(current4.parentSheetIndex.Value));
					if (flag6)
					{
						list4.Add(current4);
					}
				}
				foreach (ResourceClump item in list4)
				{
					bool flag7 = farm.resourceClumps.Contains(item);
					if (flag7)
					{
						farm.resourceClumps.Remove(item);
					}
				}
			}
			//bool flag8 = location is Woods;
			//if (flag8)
			//{
			//	Woods woods = location as Woods;
			//	List<ResourceClump> list5 = new();
			//	foreach (ResourceClump stump in woods.stumps)
			//	{
			//		bool flag9 = types == null || types.Contains(stump.GetType()) || (parentSheetIndices != null && parentSheetIndices.Contains(stump.parentSheetIndex.Value));
			//		if (flag9)
			//		{
			//			list5.Add(stump);
			//		}
			//	}
			//	foreach (ResourceClump item2 in list5)
			//	{
			//		bool flag10 = woods.stumps.Contains(item2);
			//		if (flag10)
			//		{
			//			woods.stumps.Remove(item2);
			//		}
			//	}
			//}
			bool flag11 = location is Forest;
			if (flag11)
			{
				Forest forest = location as Forest;
				bool flag12 = forest.obsolete_log != null && parentSheetIndices != null && parentSheetIndices.Contains(forest.obsolete_log.parentSheetIndex.Value);
				if (flag12)
				{
					forest.obsolete_log = null;
				}
			}
			foreach (Vector2 item3 in list)
			{
				bool flag13 = location.terrainFeatures.ContainsKey(item3);
				if (flag13)
				{
					location.terrainFeatures.Remove(item3);
				}
			}
			foreach (Vector2 item4 in list2)
			{
				bool flag14 = location.objects.ContainsKey(item4);
				if (flag14)
				{
					location.objects.Remove(item4);
				}
			}
			foreach (LargeTerrainFeature item5 in list3)
			{
				bool flag15 = location.largeTerrainFeatures.Contains(item5);
				if (flag15)
				{
					location.largeTerrainFeatures.Remove(item5);
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002980 File Offset: 0x00000B80
		public static void LoadMapFeatures(GameLocation location, Type[] types = null, int[] parentSheetIndices = null)
		{
			GameLocation gameLocation = Regenerator.MakeClone(location);
			List<ResourceClump> clumps = Regenerator.GetClumps(location);
			foreach (KeyValuePair<Vector2, TerrainFeature> pair in gameLocation.terrainFeatures.Pairs)
			{
				bool flag = (types == null || types.Contains(pair.Value.GetType()) || types.Contains(typeof(TerrainFeature))) && !location.terrainFeatures.ContainsKey(pair.Key) && !location.objects.ContainsKey(pair.Key);
				if (flag)
				{
					location.terrainFeatures[pair.Key] = pair.Value;
				}
			}
			foreach (KeyValuePair<Vector2, StardewValley.Object> pair2 in gameLocation.objects.Pairs)
			{
				bool flag2 = (types == null || types.Contains(pair2.Value.GetType()) || (parentSheetIndices != null && parentSheetIndices.Contains(pair2.Value.ParentSheetIndex)) || types.Contains(typeof(StardewValley.Object)) || (Regenerator.regeneratorOptions.ContainsKey("Forage") && Regenerator.regeneratorOptions["Forage"] && pair2.Value.isForage())) && !location.terrainFeatures.ContainsKey(pair2.Key) && !location.objects.ContainsKey(pair2.Key);
				if (flag2)
				{
					location.objects[pair2.Key] = pair2.Value;
				}
			}
			foreach (LargeTerrainFeature current3 in gameLocation.largeTerrainFeatures)
			{
				bool flag3 = (types == null || types.Contains(current3.GetType())) && !location.largeTerrainFeatures.Contains(current3);
				if (flag3)
				{
					location.largeTerrainFeatures.Add(current3);
				}
			}
			bool flag4 = location is not Farm;
			if (!flag4)
			{
				foreach (ResourceClump item in clumps)
				{
					bool flag5 = (types == null || types.Contains(item.GetType()) || (parentSheetIndices != null && parentSheetIndices.Contains(item.parentSheetIndex.Value))) && !(location as Farm).resourceClumps.Contains(item);
					if (flag5)
					{
						(location as Farm).addResourceClumpAndRemoveUnderlyingTerrain(item.parentSheetIndex.Value, item.width.Value, item.height.Value, item.Tile);
					}
				}
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public static List<ResourceClump> GetClumps(GameLocation location)
		{
			List<ResourceClump> list = new();
			FieldInfo[] fields = location.GetType().GetFields();
			FieldInfo[] array = fields;
			int i = 0;
			while (i < array.Length)
			{
				FieldInfo fieldInfo = array[i];
				bool flag = fieldInfo.FieldType == typeof(ResourceClump);
				if (flag)
				{
					list.Add(fieldInfo.GetValue(location) as ResourceClump);
				}
				else
				{
					bool flag2 = fieldInfo.FieldType is not ICollection<ResourceClump>;
					if (!flag2)
					{
						foreach (ResourceClump item in (fieldInfo as ICollection<ResourceClump>))
						{
							list.Add(item);
						}
					}
				}
				i++;
				continue;
			}
			return list;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002DBC File Offset: 0x00000FBC
		public static GameLocation MakeClone(GameLocation source)
		{
			Type type = source.GetType();
			Logger.log("Location was of type " + type.ToString(), 0);
			ConstructorInfo constructor = type.GetConstructor(new Type[]
			{
				typeof(string),
				typeof(string)
			});
			bool flag = constructor != null;
			if (flag)
			{
				Logger.log("Found a (string, string) constructor.  Invoking...", 0);
				try
				{
					GameLocation gameLocation = constructor.Invoke(new object[]
					{
						source.mapPath.Value,
                        source.Name
					}) as GameLocation;
					Logger.log("Constructed location was of type " + gameLocation.GetType().ToString(), 0);
					gameLocation.DayUpdate(Game1.dayOfMonth);
					return gameLocation;
				}
				catch (Exception)
				{
				}
			}
			ConstructorInfo constructor2 = type.GetConstructor(new Type[0]);
			bool flag2 = constructor2 != null;
			GameLocation result;
			if (flag2)
			{
				Logger.log("Found an empty constructor.  Invoking...", 0);
				GameLocation gameLocation2 = constructor2.Invoke(new object[0]) as GameLocation;
				Logger.log("Constructed location was of type " + gameLocation2.GetType().ToString(), 0);
				gameLocation2.loadObjects();
				gameLocation2.DayUpdate(Game1.dayOfMonth);
				result = gameLocation2;
			}
			else
			{
				Logger.log("Could not construct the location.  Defaulting to GameLocation class...", 0);
				result = new GameLocation(source.mapPath.Value, source.Name);
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002F40 File Offset: 0x00001140
		public static void LoadTerrainFeatures(GameLocation location, Type[] types = null) 
		{
			Dictionary<Vector2, TerrainFeature> dictionary = new();
			foreach (KeyValuePair<Vector2, TerrainFeature> pair in location.terrainFeatures.Pairs)
			{
				dictionary.Add(pair.Key, pair.Value);
			}
			location.terrainFeatures.Clear();
			Dictionary<Vector2, StardewValley.Object> dictionary2 = new();
			foreach (KeyValuePair<Vector2, StardewValley.Object> pair2 in location.objects.Pairs)
			{
				dictionary2.Add(pair2.Key, pair2.Value);
			}
			location.objects.Clear();
			List<LargeTerrainFeature> list = new();
			foreach (LargeTerrainFeature current3 in location.largeTerrainFeatures)
			{
				list.Add(current3);
			}
			location.largeTerrainFeatures.Clear();
			location.loadObjects();
			foreach (KeyValuePair<Vector2, TerrainFeature> pair3 in location.terrainFeatures.Pairs)
			{
				bool flag = types == null || types.Contains(pair3.Value.GetType());
				if (flag)
				{
					dictionary[pair3.Key] = pair3.Value;
				}
			}
			foreach (KeyValuePair<Vector2, StardewValley.Object> pair4 in location.objects.Pairs)
			{
				bool flag2 = types == null || types.Contains(pair4.Value.GetType());
				if (flag2)
				{
					dictionary2[pair4.Key] = pair4.Value;
				}
			}
			foreach (LargeTerrainFeature current4 in location.largeTerrainFeatures)
			{
				bool flag3 = (types == null || types.Contains(current4.GetType())) && !list.Contains(current4);
				if (flag3)
				{
					list.Add(current4);
				}
			}
			location.terrainFeatures.Clear();
			location.objects.Clear();
			location.largeTerrainFeatures.Clear();
			foreach (Vector2 key in dictionary.Keys)
			{
				location.terrainFeatures[key] = dictionary[key];
			}
			foreach (Vector2 key2 in dictionary2.Keys)
			{
				location.objects[key2] = dictionary2[key2];
			}
			foreach (LargeTerrainFeature item in list)
			{
				bool flag4 = !location.largeTerrainFeatures.Contains(item);
				if (flag4)
				{
					location.largeTerrainFeatures.Add(item);
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00003348 File Offset: 0x00001548
		public static GameLocation DuplicateAndGenerate(GameLocation location) 
		{
			bool flag = location.map == null;
			GameLocation result;
			if (flag)
			{
				bool flag2 = location is MineShaft;
				if (flag2)
				{
					result = new MineShaft((location as MineShaft).mineLevel);
				}
				else
				{
					result = null;
				}
			}
			else
			{
				bool flag3 = location.Name == null;
				if (flag3)
				{
					result = null;
				}
				else
				{
					bool flag4 = location is MineShaft;
					if (flag4)
					{
						result = new MineShaft((location as MineShaft).mineLevel);
					}
					else
					{
						bool flag5 = location is Town;
						if (flag5)
						{
							result = new Town(location.mapPath.Value, location.Name);
						}
						else
						{
							bool flag6 = location is Farm;
							if (flag6)
							{
								result = new Farm(location.mapPath.Value, location.Name);
							}
							else
							{
								bool flag7 = location is Woods;
								if (flag7)
								{
									Woods woods = new(location.mapPath.Value, location.Name);
									woods.DayUpdate(Game1.dayOfMonth);
									result = woods;
								}
								else
								{
									bool flag8 = location is Forest;
									if (flag8)
									{
										result = new Forest(location.mapPath.Value , location.Name);
									}
									else
									{
										result = new GameLocation(location.mapPath.Value, location.Name);
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04000005 RID: 5
		public static Dictionary<string, bool> regeneratorOptions = new();

		// Token: 0x04000006 RID: 6
		public static Dictionary<string, Type> typeKeys = new()
        {
			{
				"Bush",
				typeof(Bush)
			},
			{
				"Tree",
				typeof(Tree)
			},
			{
				"Grass",
				typeof(Grass)
			},
			{
				"ResourceClump",
				typeof(ResourceClump)
			},
			{
				"Path",
				typeof(Flooring)
			},
			{
				"Fence",
				typeof(Fence)
			},
			{
				"Soil",
				typeof(HoeDirt)
			},
			{
				"Object",
				typeof(StardewValley.Object)
			},
			{
				"TFeature",
				typeof(TerrainFeature)
			}
		};

		// Token: 0x04000007 RID: 7
		public static Dictionary<string, List<int>> indexKeys = new()
        {
			{
				"Weeds",
				new List<int>
				{
					343,
					450,
					674,
					675,
					676,
					677,
					678,
					679,
					784,
					785,
					786,
					792,
					793,
					794
				}
			},
			{
				"Twig",
				new List<int>
				{
					294,
					295
				}
			},
			{
				"Rock",
				new List<int>
				{
					450,
					343
				}
			},
			{
				"Stump",
				new List<int>
				{
					600
				}
			},
			{
				"Log",
				new List<int>
				{
					602
				}
			},
			{
				"Boulder",
				new List<int>
				{
					672
				}
			}
		};

		// Token: 0x04000008 RID: 8
		public static List<string> canGenerate = new()
        {
			"Bush",
			"Tree",
			"Grass",
			"ResourceClump",
			"Weeds",
			"Twig",
			"Rock",
			"Forage",
			"Stump",
			"Log",
			"Boulder",
			"Object",
			"TFeature"
		};
	}
}
