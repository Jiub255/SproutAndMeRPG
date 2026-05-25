extends Node2D

@export var number_one : Enemy
@export var speed : float = 800.0

func _process(delta: float) -> void:
	var direction : Vector2 = Input.get_vector(
								  'left',
								  'right',
								  'up',
								  'down').normalized();
	
	position += direction * speed * delta
	
	if Input.is_action_just_pressed('interact'):
		number_one.health -= 11
		