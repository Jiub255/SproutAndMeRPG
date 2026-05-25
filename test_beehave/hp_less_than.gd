@tool
extends ConditionLeaf

@export_range(0, 100, 1, "suffix:%") var health_percentage : int = 30

func tick(actor: Node, _blackboard: Blackboard) -> int:
	if not actor:
		return FAILURE
		
	var actor_health_percent : float = 100.0 * float(actor.health) / float(actor.max_health)
	if actor_health_percent < health_percentage:
		return SUCCESS
	return FAILURE
