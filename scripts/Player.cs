using Godot;
using System;

public partial class Player : CharacterBody3D {
    public const float Speed = 5.0f;
    public const float JumpVelocity = 4.5f;

    public override void _PhysicsProcess(double delta) {
        Vector3 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor()) {
            velocity += GetGravity() * (float)delta;
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor()) {
            velocity.Y = JumpVelocity;
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        if (direction != Vector3.Zero) {
            velocity.X = direction.X * Speed;
            velocity.Z = direction.Z * Speed;
        } else {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();

        //collision fea
        /*for (int i = 0; i < GetSlideCollisionCount(); i++) {
            KinematicCollision3D collision = GetSlideCollision(i);
            Node3D otherBody = collision.GetCollider() as Node3D;

            if (otherBody != null) {
                GD.Print("Character collided with: " + otherBody.Name);

                // You can check if the other body is a RigidBody3D or a specific type/group
                if (otherBody is RigidBody3D rigidBody) {
                    GD.Print("Collided with a RigidBody3D named: " + rigidBody.Name);
                    // Add your custom logic here (e.g., apply a force)
                }
            }
        }*/
    }

    private void OnBodyEntered(Node body) {
        GD.Print("RigidBody detected a body entered: " + body.Name);

        if (body is RigidBody3D rigidBody) {
            GD.Print("The CharacterBody3D " + rigidBody.Name + " entered the Rigidbody's collision area.");
            // Add your custom logic here (e.g., play sound, decrease health)
        }
    }
}
