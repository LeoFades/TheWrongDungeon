using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// input handler class. handle all input except for ui
    /// </summary>
    public class InputHandler
    {
        private Game _gamePanel;
        public InputHandler(Game gamePanel)
        {
            _gamePanel = gamePanel;
        }

        /// <summary>
        /// checks for all the input
        /// </summary>
        public void Check()
        {
            PlayerAttack();
            PlayerMove();
            PlayerEquip();
            PlayerInteract();
        }

        /// <summary>
        /// checks if player attack
        /// </summary>
        public void PlayerAttack()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                _gamePanel.Player.Attack();
            }
        }

        /// <summary>
        /// checks if player move
        /// </summary>
        public void PlayerMove()
        {
            if (SplashKit.KeyDown(KeyCode.WKey))
            {
                _gamePanel.Player.Move(PlayerDirection.Up);
            }
            if (SplashKit.KeyDown(KeyCode.AKey))
            {
                _gamePanel.Player.Move(PlayerDirection.Left);
            }
            if (SplashKit.KeyDown(KeyCode.SKey))
            {
                _gamePanel.Player.Move(PlayerDirection.Down);
            }
            if (SplashKit.KeyDown(KeyCode.DKey))
            {
                _gamePanel.Player.Move(PlayerDirection.Right);
            }
        }

        /// <summary>
        /// checks if player equips a weapon
        /// </summary>
        public void PlayerEquip()
        {
            if (SplashKit.KeyTyped(KeyCode.EKey))
            {
                if (_gamePanel.Player.Weapon != null)
                {
                    foreach (Weapon w in _gamePanel.AllWeapons)
                    {
                        if (_gamePanel.Player.Weapon != null && w.IsEquipped)
                        {
                            _gamePanel.Player.Weapon = w.Unequip();
                        }

                    }

                }
                else if (_gamePanel.Player.Weapon == null)
                {
                    foreach (Weapon w in _gamePanel.AllWeapons)
                    {
                        if (_gamePanel.Player.Weapon == null && !w.IsEquipped && w.IsInRange(_gamePanel.Player))
                        {
                            _gamePanel.Player.Weapon = w.Equip();
                        }

                    }

                }
            }
        }

        /// <summary>
        /// checks if player interact with a structure
        /// </summary>
        public void PlayerInteract()
        {
            if (SplashKit.KeyTyped(KeyCode.FKey))
            {
                foreach (Structure s in _gamePanel.AllStructures)
                {
                    if (s is Shop)
                    {
                        Shop shop = (Shop)s;
                        shop.Interact();
                    }
                    if (s is Exit)
                    {
                        Exit exit = (Exit)s;
                        exit.Interact();
                    }
                }
            }
        }
    }
}