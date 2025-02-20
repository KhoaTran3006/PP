using UnityEngine;

namespace PaintingBoard
{
    public interface IPainting
    {
        BoardConfig.PaintColor Color { get; }
        GameObject GameObject { get; }
        bool Usable { get; }
        void UsePaint();
    }

    public interface IPickable
    {
        GameObject GameObject { get; }

        void Pickup();
        void Drop();
    }
}
