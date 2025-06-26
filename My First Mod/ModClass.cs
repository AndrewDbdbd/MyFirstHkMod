using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using UnityEngine;

namespace MyFirstMod
{
    public class MyFirstMod : Mod
    {

        public MyFirstMod() : base("My First Mod") { }
        public override string GetVersion() => "v1";


        public override void Initialize()
        {
            ModHooks.HeroUpdateHook += OnHeroUpdate;
            ModHooks.OnEnableEnemyHook += ModHooks_OnEnableEnemyHook;
            ModHooks.AfterTakeDamageHook += AfterTakeDamage;
        }

        private bool ModHooks_OnEnableEnemyHook(GameObject enemy, bool isAlreadyDead)
        {
            Log($"found: {enemy.name}, is {(isAlreadyDead ? "dead" : "alive")}");
            return isAlreadyDead;
        }


        public void OnHeroUpdate()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Log("fg5tehjntgdvrcf");
            }
        }
        

        public int AfterTakeDamage(int hazardType, int damageAmount)
        {
            Log($"Damage amount is {damageAmount}");

            //make player take double damage
            return damageAmount * 2;
        }
    }
}
