@tool
extends ActionLeaf

enum MoveType {
	TO_TARGET = 0,
	AWAY_FROM_TARGET = 1,
}
	
@export var move_type : MoveType = MoveType.TO_TARGET

func tick(actor: Node, _blackboard: Blackboard) -> int:
	var enemy_actor : Enemy = actor
	var target : Node = get_tree().get_first_node_in_group("player")
	
	var away_direction : Vector2 = target.global_position.direction_to(actor.global_position)
	match move_type:
		MoveType.AWAY_FROM_TARGET:
			enemy_actor.move_direction =away_direction
			return SUCCESS
		
		MoveType.TO_TARGET:
			enemy_actor.move_direction =  -1 * away_direction
			return SUCCESS

	return FAILURE
