using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Tools;

//TODO
// 1 - IMPROVE MOVEMENT
//      - Movement while using certain tools
// 2 - IMPROVE TOOLS & WEAPON HANDLING
//      - Mouse click direction calculation (Override normal direction for tools 'n' weapons)


namespace CombatControls
{

    public class ModEntry : Mod
    {
        public bool isAttacking = false;
        public int attackCounter = 0;
        // Log Buttons bressed 
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += InputEvents_ButtonPressed;
            helper.Events.GameLoop.UpdateTicking += GameEvents_UpdateTick;
            Console.WriteLine("Hooked Events");
        }

        private void GameEvents_UpdateTick(object sender, EventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            if (isAttacking == true)
            {
                if (attackCounter == 0)
                {
                    isAttacking = false;
                }
                else
                {
                    attackCounter--;
                    //this.Monitor.Log($"{attackCounter}");
                }
            }
        }

        private void InputEvents_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {

            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady || isAttacking == true)
                return;


            string button = e.Button.ToString();
            //if (e.Button.IsUseToolButton() && button == "MouseLeft")
            if (e.Button == SButton.MouseLeft) 
            {

                float newVelocity = 2.4f;
                float mouseDirectionX = e.Cursor.AbsolutePixels.X - Game1.player.Position.X;
                float mouseDirectionY = e.Cursor.AbsolutePixels.Y - Game1.player.Position.Y;
                float mouseDirectionXpower = mouseDirectionX * mouseDirectionX;
                float mouseDirectionYpower = mouseDirectionY * mouseDirectionY;

                //this.monitor.Log($"{Game1.player.Name} clicked the {e.Button} button!");
                //this.monitor.Log($"{Game1.player.Name} should be moving in direction: {Game1.player.getDirection()}");


                // Is holding weapon? - isHoldingWeapon

                if (Game1.player.CurrentTool is object && Context.IsPlayerFree == true)
                {
                    string currentTool = Game1.player.CurrentTool.ToString();
                    //this.monitor.Log($"{Game1.player.Name} is holding {currentTool}");
                    if (currentTool == "StardewValley.Tools.MeleeWeapon")
                    {
                        isAttacking = true;
                        attackCounter = 15;

                        //this.monitor.Log($"{Game1.player.Name} is holding a WEAPON!.");
                        
                            if (Game1.player.getDirection() == 3)
                            {
                                Game1.player.canMove = true;
                                Game1.player.xVelocity = 0 - newVelocity;

                            }
                            if (Game1.player.getDirection() == 1)
                            {
                                Game1.player.canMove = true;
                                Game1.player.xVelocity = newVelocity;

                            }
                            if (Game1.player.getDirection() == 2)
                            {
                                Game1.player.canMove = true;
                                Game1.player.yVelocity = 0 - newVelocity;

                            }
                            if (Game1.player.getDirection() == 0)
                            {
                                Game1.player.canMove = true;
                                Game1.player.yVelocity = newVelocity;

                            }
                        
                    }


                
                

                    if (mouseDirectionXpower > mouseDirectionYpower)
                    {
                        if (mouseDirectionX < 0)
                        {
                            //left
                            Game1.player.FacingDirection = 3;
                            //if (isHoldingWeapon == true)
                            //{
                            //    Game1.player.canMove = true;
                            //    Game1.player.xVelocity = 0 - newVelocity;
                            //    this.Monitor.Log($"{Game1.player.Name} should be moving!");
                            //}
                        }
                        else
                        {
                            //right
                            Game1.player.FacingDirection = 1;
                            //if (isHoldingWeapon == true)
                            //{
                            //    Game1.player.canMove = true;
                            //    Game1.player.xVelocity = newVelocity;
                            //    this.Monitor.Log($"{Game1.player.Name} should be moving!");
                            //}
                        }

                    }
                    else
                    {
                        //up or down
                        if (mouseDirectionY < 0)
                        {
                            //up
                            Game1.player.FacingDirection = 0;
                            //if (isHoldingWeapon == true)
                            //{
                            //    Game1.player.canMove = true;
                            //    Game1.player.yVelocity = newVelocity;
                            //    this.Monitor.Log($"{Game1.player.Name} should be moving!");
                            //}
                        }
                        else
                        {
                            //down
                            Game1.player.FacingDirection = 2;
                            //if (isHoldingWeapon == true)
                            //{
                            //    Game1.player.canMove = true;
                            //    Game1.player.yVelocity = 0 - newVelocity;
                            //    this.Monitor.Log($"{Game1.player.Name} should be moving!");
                            //}
                        }
                    
                }


                }
                else
                {
                    // print button presses to the console window
                    //this.monitor.Log($"{Game1.player.Name} is at position (X/Y):{Game1.player.Position.X}/{Game1.player.Position.Y}");
                    //this.monitor.Log($"and pressed {e.Button}.");

                }
                //Melee Combat movement


                if (Context.IsPlayerFree == true)
                {
                    //this.monitor.Log($"{Game1.player.Name} is free.");
                }
                else
                {
                    //this.monitor.Log($"{Game1.player.Name} is not free.");
                }


            }

        }
    }
}