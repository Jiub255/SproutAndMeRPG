using System;
using System.Collections.Generic;

public class BehaviorTest
{
    public BTManager BehaviorManager { get; private set; }

    // TODO: Use a struct with delegates instead?
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
