using Godot;
using System;

public partial class Animal : RigidBody2D
{

	private readonly Vector2 DRAG_LIM_MIN = new Vector2(-60, 0);
	private readonly Vector2 DRAG_LIM_MAX = new Vector2(0, 60);

	private const float IMPULSE_MULT = 20.0f;

	[Export] private Label _label;
	[Export] private AudioStreamPlayer2D _stretchSound;
	[Export] private AudioStreamPlayer2D _launchSound;
	[Export] private AudioStreamPlayer2D _kickSound;

	private bool _isDragging = false;
	private Vector2 _dragStart = Vector2.Zero;
	private Vector2 _draggedVector = Vector2.Zero;
	private Vector2 _start = Vector2.Zero;
	private bool _isDead = false;

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionReleased("drag") && _isDragging)
		{
			CallDeferred(nameof(HandleRelease));
		}
    }

		public override void _Ready()
	{
		InputEvent += OnInputEvent;
		SleepingStateChanged += OnSleepingStateChanged;
		_start = Position;
	}

    private void OnSleepingStateChanged()
    {
        if (!Sleeping) return;

		foreach (var body in GetCollidingBodies())
		{
			if (body is Cup cup)
			{
				cup.Die();
			}
		}
		Die();
    }


    public override void _Process(double delta)
	{
		if (_isDragging)
		{
			HandleDragging();
		}
		UpdateDebug();
	}

	private void UpdateDebug()
	{
		string ds = $"SL: {Sleeping} FR: {Freeze}\n DragStart: {_isDragging} Start: {_start}\n DragVec: {_draggedVector}";
		_label.Text = ds;
	}

	private void StartDragging()
	{
		_isDragging = true;
		_dragStart = GetGlobalMousePosition();
	}

	private Vector2 CalculateImpulse()
	{
		return _draggedVector * -IMPULSE_MULT;
	}

	private void HandleRelease()
	{
		_isDragging = false;
		_launchSound.Play();
		Freeze = false;
		ApplyCentralImpulse(CalculateImpulse());
		SignalHub.EmitOnAttemptMade();
	}

	private void HandleDragging()
	{
		_draggedVector = GetGlobalMousePosition() - _dragStart;
		_draggedVector = _draggedVector.Clamp(
			DRAG_LIM_MIN, DRAG_LIM_MAX
		);
		Position = _start + _draggedVector;
	}

	private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
    {
        if (@event.IsActionPressed("drag"))
		{
			InputEvent -= OnInputEvent;
			StartDragging();
		}
    }

	public void Die()
	{
		if(_isDead) return;
		_isDead = true;
		SignalHub.EmitOnAnimalDied();
		QueueFree();		
	}

}
