namespace CutilloRigby.Device;

public abstract class Register
{
    protected byte _value;

    protected readonly Func<byte> _getValue;
    protected readonly Action<byte> _onWriteValue;

    protected Register(Func<byte> getValue, Action<byte> onWriteValue)
    {
        _getValue = getValue;
        _onWriteValue = onWriteValue;
    }

    protected void Load()
    {
        _value = _getValue();
    }

    protected byte ReadValue(byte readMask)
    {
        return (byte)(_value & readMask);
    }
    protected void GetWriteValue(byte value, byte writeMask)
    {
        _value &= writeMask;
        _value |= value;

        _onWriteValue(_value);
    }

    protected T ReadValue<T>(byte readMask)
        where T : Enum
    {
        return (T)Enum.ToObject(typeof(T), _value & readMask);
    }
    protected void GetWriteValue<T>(T value, byte writeMask)
        where T : Enum
    {
        _value &= writeMask;
        _value |= Convert.ToByte(value);

        _onWriteValue(_value);
    }
}
