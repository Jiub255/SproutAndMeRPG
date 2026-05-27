using System;
using System.Collections.Generic;

namespace SproutandMeRPG.characters.behavior_tree;

public class BehaviorTest
{
    public BTManager BehaviorManager { get; private set; }

    public BehaviorTest(Dictionary<string, Delegate> actions)
    {
        BehaviorManager = new BTManager(tree =>
            {
                BTBuilder builder = new(tree);
                builder
                    .Selector("TestChecklist")
                        .Sequence("Health")
                            .Condition("Injured", (Func<BTResult>)actions["Injured"])
                            .Action("LocateShelter", (Func<BTResult>)actions["LocateShelter"])
                        .End()
                    .End();
                return builder.Build();
            },
            dangerInterrupt: (Func<BTResult>)actions["DangerNearby"]
        );
    }
}
