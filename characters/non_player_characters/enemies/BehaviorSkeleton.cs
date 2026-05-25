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
                .Condition("Wandering", (Func<BTResult>)actions["Wandering"])
                    .Sequence("WanderSequence")
                        .Action("Wander", (Func<BTResult>)actions["Wander"])
                        .Condition("InChaseRange", (Func<BTResult>)actions["InChaseRange"])
                            .Action("ChangeToChase", (Func<BTResult>)actions["ChangeToChase"])
                    .End()
                .Condition("Chasing", (Func<BTResult>)actions["Chasing"])
                    .Action("Chase", (Func<BTResult>)actions["Chase"])
                .Condition("InCombat", (Func<BTResult>)actions["InCombat"])
            
            .End();
        return builder;
    }
}
    
    
// Selector (which general state you're in)
    // Wander state (sequence)
        // Action (wander)
        // Condition (player in chase range)
            // Action (Go toward player (go to chase state))
    // Chase state (sequence)
        // Action (Go toward player)
        // Condition (player in attack range)
            // Action (go to combat state)
    // Combat state (selector)
        // Condition (0 hp)
            // Action (die)
        // Condition (player out of range)
            // Action (go to chase state)
        // Condition (attack timer done)
            // Sequence
                // Action (attack player)
                // Action (reset attack timer)
                // Condition (player dead)
                    // Action (return to wander state)
