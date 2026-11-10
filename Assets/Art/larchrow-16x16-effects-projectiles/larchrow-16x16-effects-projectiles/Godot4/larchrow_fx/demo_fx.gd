# Larchrow Effects & Projectiles - demo for Godot 4.
# Replays the one-shot effects in the grid, and fires a fireball ("Shot") that turns into a large explosion ("Boom").
extends Node2D

@export var shot_speed := 90.0
@export var shot_start := Vector2(20, 186)
@export var shot_end_x := 270.0
@onready var shot: AnimatedSprite2D = $Shot
@onready var boom: AnimatedSprite2D = $Boom

func _ready() -> void:
	for c in get_children():
		if c is AnimatedSprite2D and c != shot and c != boom:
			c.animation_finished.connect(_replay.bind(c))
	boom.animation_finished.connect(_reset_shot)
	_reset_shot()

func _replay(s: AnimatedSprite2D) -> void:
	await get_tree().create_timer(0.5).timeout
	s.set_frame_and_progress(0, 0.0)
	s.play()

func _reset_shot() -> void:
	boom.visible = false
	shot.position = shot_start
	shot.visible = true
	shot.play("fly_right")

func _process(delta: float) -> void:
	if shot.visible:
		shot.position.x += shot_speed * delta
		if shot.position.x >= shot_end_x:
			shot.visible = false
			boom.position = shot.position
			boom.visible = true
			boom.set_frame_and_progress(0, 0.0)
			boom.play("explode")
