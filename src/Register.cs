namespace CutilloRigby.Device;

public abstract class RegisterBase
{
    protected byte _value;

    protected readonly Func<byte> _getValue;
    protected readonly Action<byte> _onWriteValue;

    protected RegisterBase(Func<byte> getValue, Action<byte> onWriteValue)
    {
        _getValue = getValue;
        _onWriteValue = onWriteValue;

        _value = _getValue();
    }

    public void Load()
    {
        _value = _getValue();
    }

    protected byte this[byte mask]
    {
        get => (byte)(_value & mask);
        set
        {
            _value &= (byte)~mask;
            _value |= value;

            _onWriteValue(_value);
        }
    }

    public byte Value => _value;
}
