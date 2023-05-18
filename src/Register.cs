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

    protected Func<byte> GetReadValue(byte readMask)
    {
        return () => (byte)(_value & readMask);
    }
    protected Action<byte> GetWriteValue(byte writeMask)
    {
        return value => {
            _value &= writeMask;
            _value |= value;

            _onWriteValue(_value);
        };
    }

    protected Func<T> GetReadValue<T>(byte readMask)
        where T : Enum
    {
        return () => (T)Enum.ToObject(typeof(T), _value & readMask);
    }
    protected Action<T> GetWriteValue<T>(byte writeMask)
        where T : Enum
    {
        return value => {
            _value &= writeMask;
            _value |= Convert.ToByte(value);

            _onWriteValue(_value);
        };
    }
}
