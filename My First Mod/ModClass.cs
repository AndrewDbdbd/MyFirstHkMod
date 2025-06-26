using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static MonoMod.Cil.RuntimeILReferenceBag.FastDelegateInvokers;
using Vector2 = UnityEngine.Vector2;

namespace MyFirstMod
{
    
    public class MyFirstMod : Mod, IMenuMod
    {

        public MyFirstMod() : base("My First Mod") { }
        public override string GetVersion() => "v1";

        private int optionOne;
        private bool optionTwo;

        public bool ToggleButtonInsideMenu => true;

        // The rest of the class... 

        public List<IMenuMod.MenuEntry> GetMenuData(IMenuMod.MenuEntry? toggleButtonEntry)
        {
            return new List<IMenuMod.MenuEntry>
        {
            new IMenuMod.MenuEntry {
                Name = "My First Option",
                Description = "Will be displayed in small text",
                Values = new string[] {
                    "ZeroDash",
                    "NewDash"
                },
                // opt will be the index of the option that has been chosen
                Saver = opt => this.optionOne = opt,

                Loader = () => this.optionOne 
            },
            new IMenuMod.MenuEntry {
                Name = "My Second Option",
                // Nothing will be displayed
                Description = null,
                Values = new string[] {
                    "Off",
                    "On"
                },
                Saver = opt => this.optionTwo = opt switch {
                    0 => false,
                    1 => true,
                    // This should never be called
                    _ => throw new InvalidOperationException()
                },
                Loader = () => this.optionTwo switch {
                    false => 0,
                    true => 1,
                }
            }
        };
    }
         
        public override void Initialize()
        {
            ModHooks.DashVectorHook += ModHooks_DashVectorHook;
            ModHooks.SceneChanged += ModHooks_SceneChanged;
        }

        private void ModHooks_SceneChanged(string obj)
        {
            Log(obj);
        }
        private Vector2 ModHooks_DashVectorHook(Vector2 arg)
        {
            switch (this.optionOne)
            {
                case 0: { return ChangeVector(arg, ZeroDash); }
                case 1: { return ChangeVector(arg, NewDash); }
            }
            return arg;
        }
        private Vector2 ChangeVector(Vector2 arg, System.Func<Vector2, Vector2> op) { return op(arg); }
        Vector2 ZeroDash(Vector2 arg) { return arg; }
        Vector2 NewDash(Vector2 arg)
        {
            Vector2 vector = new Vector2(7f * (arg.x > 0 ? 1 : -1), 18f);
            return vector;
        }



        //private bool ModHooks_OnEnableEnemyHook(GameObject enemy, bool isAlreadyDead)
        //{
        //    Log($"found: {enemy.name}, is {(isAlreadyDead ? "dead" : "alive")}");
        //    return isAlreadyDead;
        //}


        //public void OnHeroUpdate()
        //{
        //    if (Input.GetKeyDown(KeyCode.U))
        //    {
        //        Log("fg5tehjntgdvrcf");
        //    }
        //}


        //public int AfterTakeDamage(int hazardType, int damageAmount)
        //{
        //    Log($"Damage amount is {damageAmount}");

        //    //make player take double damage
        //    return damageAmount * 2;
        //}




    }
}
