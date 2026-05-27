using System;
using System.Collections.Generic;
using SproutandMeRPG.characters.behavior_tree;

namespace SproutandMeRPG.characters.non_player_characters.enemies;

public class BehaviorSkeleton(
    Dictionary<string, Delegate> actions,
    Func<BTResult> dangerInterrupt)
    : Behavior(actions, dangerInterrupt)
{
    protected override BTBuilder CreateBuilder(BTManager tree, Dictionary<string, Delegate> actions)
    {
        BTBuilder builder = new(tree);
        builder
            .Selector("Check State")
                .Sequence("Chasing")
                    .Condition("Chasing", (Func<BTResult>)actions["Chasing"])
                    .Action("Chase", (Func<BTResult>)actions["Chase"])
                .End()
                .Sequence("Wandering")
                    .Condition("Wandering", (Func<BTResult>)actions["Wandering"])
                    .Action("Wander", (Func<BTResult>)actions["Wander"])
                .End()
                .Sequence("Combat")
                    .Condition("InCombat", (Func<BTResult>)actions["InCombat"])
       //             .Selector("Check healths")
     //                   .Condition("PlayerDead", (Func<BTResult>)actions["PlayerDead"])
                        //.Action("Succeeder", () => BTResult.Success) // Might not need this at all, get success from condition
     //                   .Condition("SkeletonDead",  (Func<BTResult>)actions["SkeletonDead"])
                        // Same here, for now at least. Eventually go to a death state
      //                  .Sequence("Attack")
                            // tick action needed? or tick in Skeleton.process?
      //                      .Action("TickAttackTimer", (Func<BTResult>)actions["TickAttackTimer"])
       //                     .Sequence("Actual attack")
                                // Condition (attack timer done)
                                // Sequence
                                    // Action (attack player)
                                    // Action (reset attack timer)
        //                    .End()
       //                 .End()
         //           .End()
                .End()
            .End();
        return builder;
    }
}


// Selector (which general state you're in)
    // Wander state (sequence)
        // Condition (in wander state)
        // Action (wander)
    // Chase state (sequence)
        // Condition (in chase state)
        // Action (Go toward player)
    // Combat state (sequence)
        // Condition (in combat state)
        // Selector (check healths)
            // Condition (player dead)
                // Action (return to wander state, ie make sure player is null)
                                                  //(Maybe use a 'succeeder' here to instantly stop the selector)
            // Condition (0 hp)
                // Action (die)                     (Maybe use a 'succeeder' here to instantly stop the selector,
                                                    // or go to death state then queue_free)
            // Sequence (attack sequence)
                // Action (tick attack timer)
                // Sequence (actual attack)
                    // Condition (attack timer done)
                    // Sequence
                        // Action (attack player)
                        // Action (reset attack timer)
                
