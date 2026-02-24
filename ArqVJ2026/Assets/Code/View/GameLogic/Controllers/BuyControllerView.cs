using System;
using System.Collections.Generic;

namespace ZooArchitect.View.Controller
{
    internal sealed class BuyControllerView : ControllerView
    {
        private bool isOpen;
        private bool shouldOpen;

        public BuyControllerView()
        {
            isOpen = false;
            shouldOpen = false;
        }

        public override void OnSelect()
        {
            if (!isOpen)
            {
                shouldOpen = true;
            }
        }

        public override void Tick(float deltaTime)
        {
            if (shouldOpen)
            {
                CreateController();
                shouldOpen = false;
                isOpen = true;
            }
        }

        public override void CreateController()
        {
            Dictionary<string, Action> controls = new Dictionary<string, Action>();

            controls.Add("Animal food", () =>
            {
                isOpen = false;
            });

            controls.Add("Visitor food", () =>
            {
                isOpen = false;
            });

            controls.Add("workers", () =>
            {
                isOpen = false;
            });

            Display(controls, "BUY");
        }

        public override void Dispose()
        {
        }
    }
}
