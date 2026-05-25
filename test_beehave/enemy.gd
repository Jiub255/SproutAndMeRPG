class_name Enemy
extends Node2D


@export var health_label : Label
@export var max_health : int = 100
@export var speed : int = 800

var health : int = 100:
	set(value):
		health = value
		health_label.text = str(health)
		if health <= 0:
			queue_free()

var move_direction : Vector2 = Vector2.ZERO

func _ready() -> void:
	health = max_health

func _process(delta: float) -> void:
	position += move_direction * speed * delta
	
