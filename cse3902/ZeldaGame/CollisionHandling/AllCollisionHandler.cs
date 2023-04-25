using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Threading.Tasks;

namespace ZeldaGame
{

    public class AllCollisionHandler
    {
        private Dictionary<string, Tuple<ICollisionCommand, ICollisionCommand>> CollisionResponses;

        public AllCollisionHandler()
        {
            CollisionResponses = new Dictionary<string, Tuple<ICollisionCommand, ICollisionCommand>>();

            // ---------- PLAYER TO BLOCK COLLISIONS ------------- //   
            // TODO: We must write "as ICollisionCommand" for every command or else the tuple throws an error. Does this need to be fixed?
            // Player to solid block collision
            CollisionResponses.Add("Player-Block-Top", Tuple.Create(new PlayerBlockTopCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-Block-Bottom", Tuple.Create(new PlayerBlockBottomCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-Block-Left", Tuple.Create(new PlayerBlockLeftCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-Block-Right", Tuple.Create(new PlayerBlockRightCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));

            // Water collides differenty. Player/Enemies cant pass through it but items can
            CollisionResponses.Add("Player-WaterBlock-Top", Tuple.Create(new PlayerBlockTopCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-WaterBlock-Bottom", Tuple.Create(new PlayerBlockBottomCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-WaterBlock-Left", Tuple.Create(new PlayerBlockLeftCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-WaterBlock-Right", Tuple.Create(new PlayerBlockRightCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));

            // No collisions necessary for TransparentBlocks

            // ---------- PLAYER TO COLLECTABLE-ITEM COLLISIONS ------------ //
            CollisionResponses.Add("Player-CollectableItem-Top", Tuple.Create(new DoNothingCommand() as ICollisionCommand, new ItemPickupCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-CollectableItem-Bottom", Tuple.Create(new DoNothingCommand() as ICollisionCommand, new ItemPickupCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-CollectableItem-Left", Tuple.Create(new DoNothingCommand() as ICollisionCommand, new ItemPickupCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-CollectableItem-Right", Tuple.Create(new DoNothingCommand() as ICollisionCommand, new ItemPickupCommand() as ICollisionCommand));

            // ---------- PLAYER TO PURCHASABLE-ITEM COLLISIONS ------------ //
            CollisionResponses.Add("Player-PurchasableItem-Top", Tuple.Create(new DoNothingCommand() as ICollisionCommand, new ItemPurchaseCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-PurchasableItem-Bottom", Tuple.Create(new DoNothingCommand() as ICollisionCommand, new ItemPurchaseCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-PurchasableItem-Left", Tuple.Create(new DoNothingCommand() as ICollisionCommand, new ItemPurchaseCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-PurchasableItem-Right", Tuple.Create(new DoNothingCommand() as ICollisionCommand, new ItemPurchaseCommand() as ICollisionCommand));

            // ---------- PLAYER TO WIN AND GRAB TRIFORCEPIECE ------------ //
            CollisionResponses.Add("Player-TriforcePiece-Top", Tuple.Create(new PlayerWinCommand() as ICollisionCommand, new ItemPickupCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-TriforcePiece-Bottom", Tuple.Create(new PlayerWinCommand() as ICollisionCommand, new ItemPickupCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-TriforcePiece-Left", Tuple.Create(new PlayerWinCommand() as ICollisionCommand, new ItemPickupCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-TriforcePiece-Right", Tuple.Create(new PlayerWinCommand() as ICollisionCommand, new ItemPickupCommand() as ICollisionCommand));

            // --------- PLAYER TO ENEMY COLLISIONS ----------- //
            CollisionResponses.Add("Player-Enemy-Top", Tuple.Create(new PlayerTakeDamageCommand() as ICollisionCommand, new EnemyBlockBottom() as ICollisionCommand));
            CollisionResponses.Add("Player-Enemy-Bottom", Tuple.Create(new PlayerTakeDamageCommand() as ICollisionCommand, new EnemyBlockTop() as ICollisionCommand));
            CollisionResponses.Add("Player-Enemy-Left", Tuple.Create(new PlayerTakeDamageCommand() as ICollisionCommand, new EnemyBlockRight() as ICollisionCommand));
            CollisionResponses.Add("Player-Enemy-Right", Tuple.Create(new PlayerTakeDamageCommand() as ICollisionCommand, new EnemyBlockLeft() as ICollisionCommand));
            // Player to Enemy projectiles
            CollisionResponses.Add("Player-EnemyProjectile-Top", Tuple.Create(new PlayerTakeDamageCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-EnemyProjectile-Bottom", Tuple.Create(new PlayerTakeDamageCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-EnemyProjectile-Left", Tuple.Create(new PlayerTakeDamageCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-EnemyProjectile-Right", Tuple.Create(new PlayerTakeDamageCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));

            // ---------------- PLAYER TO DOOR ---------------- //
            // Player to wall collsions taken care of with the Player-Block collision response

            // Player to walkable door
            CollisionResponses.Add("Player-WalkableDoor-Top", Tuple.Create(new TeleportPlayerUpCommand() as ICollisionCommand, new TransitionUpCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-WalkableDoor-Bottom", Tuple.Create(new TeleportPlayerDownCommand() as ICollisionCommand, new TransitionDownCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-WalkableDoor-Left", Tuple.Create(new TeleportPlayerLeftCommand() as ICollisionCommand, new TransitionLeftCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-WalkableDoor-Right", Tuple.Create(new TeleportPlayerRightCommand() as ICollisionCommand, new TransitionRightCommand() as ICollisionCommand));

            // Player to stairs(for basement) //
            CollisionResponses.Add("Player-Stairs-Left", Tuple.Create(new EnterBasementCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-Stairs-Top", Tuple.Create(new LeaveBasementCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));

            // Player to explodable door (This acts like the normal player to block)
            CollisionResponses.Add("Player-BombableWall-Top", Tuple.Create(new PlayerBlockTopCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-BombableWall-Bottom", Tuple.Create(new PlayerBlockBottomCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-BombableWall-Left", Tuple.Create(new PlayerBlockLeftCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-BombableWall-Right", Tuple.Create(new PlayerBlockRightCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));

            // Player Bomb to explodable door
            CollisionResponses.Add("Explosive-BombableWall-Top", Tuple.Create(new ItemImpactCommand() as ICollisionCommand, new ExplodeDoorCommand() as ICollisionCommand));
            CollisionResponses.Add("Explosive-BombableWall-Bottom", Tuple.Create(new ItemImpactCommand() as ICollisionCommand, new ExplodeDoorCommand() as ICollisionCommand));
            CollisionResponses.Add("Explosive-BombableWall-Left", Tuple.Create(new ItemImpactCommand() as ICollisionCommand, new ExplodeDoorCommand() as ICollisionCommand));
            CollisionResponses.Add("Explosive-BombableWall-Right", Tuple.Create(new ItemImpactCommand() as ICollisionCommand, new ExplodeDoorCommand() as ICollisionCommand));

            // Player to locked door
            CollisionResponses.Add("Player-LockedDoor-Top", Tuple.Create(new PlayerBlockTopCommand() as ICollisionCommand, new PlayerLockedDoorTopCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-LockedDoor-Bottom", Tuple.Create(new PlayerBlockBottomCommand() as ICollisionCommand, new PlayerLockedDoorBottomCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-LockedDoor-Left", Tuple.Create(new PlayerBlockLeftCommand() as ICollisionCommand, new PlayerLockedDoorLeftCommand() as ICollisionCommand));
            CollisionResponses.Add("Player-LockedDoor-Right", Tuple.Create(new PlayerBlockRightCommand() as ICollisionCommand, new PlayerLockedDoorRightCommand() as ICollisionCommand));

            // ---------- ENEMY TO ENEMY COLLISIONS ----------- //
            CollisionResponses.Add("Enemy-Enemy-Top", Tuple.Create(new EnemyBlockTop() as ICollisionCommand, new EnemyBlockBottom() as ICollisionCommand));
            CollisionResponses.Add("Enemy-Enemy-Bottom", Tuple.Create(new EnemyBlockBottom() as ICollisionCommand, new EnemyBlockTop() as ICollisionCommand));
            CollisionResponses.Add("Enemy-Enemy-Left", Tuple.Create(new EnemyBlockLeft() as ICollisionCommand, new EnemyBlockRight() as ICollisionCommand));
            CollisionResponses.Add("Enemy-Enemy-Right", Tuple.Create(new EnemyBlockRight() as ICollisionCommand, new EnemyBlockLeft() as ICollisionCommand));
           
            // ---------- ENEMY TO BLOCK COLLISIONS ----------- //
            // Enemy to block
            CollisionResponses.Add("Enemy-Block-Top", Tuple.Create(new EnemyBlockTop() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Enemy-Block-Bottom", Tuple.Create(new EnemyBlockBottom() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Enemy-Block-Left", Tuple.Create(new EnemyBlockLeft() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Enemy-Block-Right", Tuple.Create(new EnemyBlockRight() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));

            // Enemy to walkable door
            CollisionResponses.Add("Enemy-WalkableDoor-Top", Tuple.Create(new EnemyBlockTop() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Enemy-WalkableDoor-Bottom", Tuple.Create(new EnemyBlockBottom() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Enemy-WalkableDoor-Left", Tuple.Create(new EnemyBlockLeft() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Enemy-WalkableDoor-Right", Tuple.Create(new EnemyBlockRight() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));

            // ---------- ENEMY TO WATER BLOCK COLLISIONS ----------- //
            // Water blocks collide differently than normal blocks. Players/Enemies cant pass through it but items can
            CollisionResponses.Add("Enemy-WaterBlock-Top", Tuple.Create(new EnemyBlockTop() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Enemy-WaterBlock-Bottom", Tuple.Create(new EnemyBlockBottom() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Enemy-WaterBlock-Left", Tuple.Create(new EnemyBlockLeft() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Enemy-WaterBlock-Right", Tuple.Create(new EnemyBlockRight() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));

            // --------- ENEMY TO PLAYER-ITEM COLLISIONS ----------- //
            // Enemy to Player Sword
            CollisionResponses.Add("Enemy-PlayerSword-Top", Tuple.Create(new EnemyTakeSwordDamageCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Enemy-PlayerSword-Bottom", Tuple.Create(new EnemyTakeSwordDamageCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Enemy-PlayerSword-Left", Tuple.Create(new EnemyTakeSwordDamageCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("Enemy-PlayerSword-Right", Tuple.Create(new EnemyTakeSwordDamageCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));

            // Enemy to Player Projectile
            CollisionResponses.Add("Enemy-PlayerProjectile-Top", Tuple.Create(new EnemyTakeOtherDamageCommand() as ICollisionCommand, new ItemImpactCommand() as ICollisionCommand));
            CollisionResponses.Add("Enemy-PlayerProjectile-Bottom", Tuple.Create(new EnemyTakeOtherDamageCommand() as ICollisionCommand, new ItemImpactCommand() as ICollisionCommand));
            CollisionResponses.Add("Enemy-PlayerProjectile-Left", Tuple.Create(new EnemyTakeOtherDamageCommand() as ICollisionCommand, new ItemImpactCommand() as ICollisionCommand));
            CollisionResponses.Add("Enemy-PlayerProjectile-Right", Tuple.Create(new EnemyTakeOtherDamageCommand() as ICollisionCommand, new ItemImpactCommand() as ICollisionCommand));

            // Enemy to Explosive
            CollisionResponses.Add("Enemy-Explosive-Top", Tuple.Create(new EnemyTakeOtherDamageCommand() as ICollisionCommand, new ItemImpactCommand() as ICollisionCommand));
            CollisionResponses.Add("Enemy-Explosive-Bottom", Tuple.Create(new EnemyTakeOtherDamageCommand() as ICollisionCommand, new ItemImpactCommand() as ICollisionCommand));
            CollisionResponses.Add("Enemy-Explosive-Left", Tuple.Create(new EnemyTakeOtherDamageCommand() as ICollisionCommand, new ItemImpactCommand() as ICollisionCommand));
            CollisionResponses.Add("Enemy-Explosive-Right", Tuple.Create(new EnemyTakeOtherDamageCommand() as ICollisionCommand, new ItemImpactCommand() as ICollisionCommand));

            // --------- PLAYER-ITEM TO BLOCK COLLISIONS -------------- //
          //  CollisionResponses.Add("PlayerProjectile-Block-Top", Tuple.Create(new ItemImpactCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
           // CollisionResponses.Add("PlayerProjectile-Block-Bottom", Tuple.Create(new ItemImpactCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
          //  CollisionResponses.Add("PlayerProjectile-Block-Left", Tuple.Create(new ItemImpactCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
           // CollisionResponses.Add("PlayerProjectile-Block-Right", Tuple.Create(new ItemImpactCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
          
            // --------- PROJECTILE TO BLOCK COLLISIONS ------------- //
            CollisionResponses.Add("EnemyProjectile-Block-Top", Tuple.Create(new ProjectileBlockCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("EnemyProjectile-Block-Bottom", Tuple.Create(new ProjectileBlockCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("EnemyProjectile-Block-Left", Tuple.Create(new ProjectileBlockCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
            CollisionResponses.Add("EnemyProjectile-Block-Right", Tuple.Create(new ProjectileBlockCommand() as ICollisionCommand, new DoNothingCommand() as ICollisionCommand));
        }

        public void HandleCollision(ICollidable object1, ICollidable object2, Side side, Rectangle overlap)
        {
            //need the command dictionary to decide which of command reponse to the collision
            string Object1Type = object1.GetCollidableType();
            //need the getCollidable method to get the type of object
            string Object2Type = object2.GetCollidableType();
            string sideString = side.ToString();
            string key = Object1Type + "-" + Object2Type + "-" + sideString;

            if (CollisionResponses.ContainsKey(key))
            {
                CollisionResponses[key].Item1.Execute(object1, object2, overlap); // Does collision action for obj1
                CollisionResponses[key].Item2.Execute(object2, object2, overlap); // Does collision action for obj2
            }  
        }
    }
}