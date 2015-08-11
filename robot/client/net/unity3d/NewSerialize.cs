using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;

namespace ExFormatter
{
    /// <summary>
    /// This supports Inheritance, New Types, Multidimenstional Arrays, Attributes.
    /// Attributes does not inherited by class, but inherited as instances types.
    /// ExBinaryFormatter class has high priority than IExFormatter interface.
    /// This does not support dynamically fields changing.
    /// If it inheritance ExBinaryFormatter, then it uses default FromByteArray and
    /// ToByteArray methods, but you can override them to get your own specific formatter,
    /// But we don't recommend this, It is better to use IExFormatter interface in cases
    /// That you need to register new data types with my class, But we recommend to use
    /// overriden in cases that you need to change one of the default methods such as Size,
    /// FromByteArray or ToByteArray but still use other defaults.
    /// If you want to use CheckSize system, then set its attribute for class, but take care
    /// if you use inheritance then you can use base class CheckSize methods, if any is available.
    /// This allows you to don't forgotten any changes in the class fields at development cycle.
    /// Also take care that you initialize all instance and non instance members for all fields,
    /// such as arrays, for doing this it is better to use constructors and if you don't do this
    /// then you encounter an exception.
    /// You can switch between field sorting and selection types by using attributes.
    /// Duplicated names at inheritance are supported.
    /// 

    [Serializable]
    public class ExFormatterBinary
    {
        private static ExFormatterBinary _value = null;
        public static ExFormatterBinary getInstance()
        {
            if (_value == null)
                _value = new ExFormatterBinary();
            return _value;
        }

        public virtual int Size
        {
            get
            {
                return _size(this);
            }
        }
		
        private void _IterateIndices(ref Int64[] indices, int[] lowerBounds, int[] upperBounds, int currentRank)
        {
            indices[currentRank]++;
            if (indices[currentRank] > upperBounds[currentRank])
            {
                indices[currentRank] = lowerBounds[currentRank];
                if (currentRank > 0)
                    _IterateIndices(ref indices, lowerBounds, upperBounds, currentRank - 1);
            }
        }
        
		protected virtual int CheckSize()
        {
            return 0;
        }
		
		private Byte[] _ConvertToStreamByte(object mainData, Type dsType)
        {
            List<Byte> data = new List<Byte>();

            if (dsType.Equals(typeof(SByte)))
            {
                Byte temp = 0;
                object value = null;
                value = mainData;
                temp = (Byte)(SByte)value;
                data.Add(temp);
            }
            else if (dsType.Equals(typeof(Boolean)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((Boolean)mainData);
                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);
            }
            else if (dsType.Equals(typeof(Char)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((Char)mainData);
                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);
            }
            else if (dsType.Equals(typeof(Byte)))
            {
                Byte temp = 0;
                temp = (Byte)mainData;

                data.Add(temp);
            }
            else if (dsType.Equals(typeof(Double)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((Double)mainData);

                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);

            }
            else if (dsType.Equals(typeof(Int16)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((Int16)mainData);

                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);
            }
            else if (dsType.Equals(typeof(Int32)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((Int32)mainData);

                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);
            }
            else if (dsType.Equals(typeof(Int64)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((Int64)mainData);

                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);
            }
            else if (dsType.Equals(typeof(Single)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((Single)mainData);

                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);
            }
            else if (dsType.Equals(typeof(UInt16)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((UInt16)mainData);

                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);
            }
            else if (dsType.Equals(typeof(UInt32)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((UInt32)mainData);

                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);
            }
            else if (dsType.Equals(typeof(UInt64)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((UInt64)mainData);

                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);
            }
            else if (dsType.IsArray)
            {
                Byte[] temp = null;
                Array array = (Array)mainData;
                int rank = array.Rank;
                Int64[] indices = new Int64[rank];
                int[] lowerBounds = new int[rank];
                int[] upperBounds = new int[rank];

                for (int i = 0; i < rank; i++)
                {
                    lowerBounds[i] = array.GetLowerBound(i);
                    upperBounds[i] = array.GetUpperBound(i);
                    indices[i] = lowerBounds[i];
                }

                for (int i = 0; i < array.Length; i++)
                {
                    object item = array.GetValue(indices);
                    temp = _ConvertToStreamByte(item, item.GetType());
                    _IterateIndices(ref indices, lowerBounds, upperBounds, rank - 1);
                    for (int j = 0; j < temp.Length; j++)
                        data.Add(temp[j]);
                }
            }
            else if (dsType.BaseType != null && dsType.BaseType.Equals(typeof(ExFormatterBinary)))
            {
                bool checkSize = false;
                SortType sortType = SortType.SortByOrder;
                BindingFlags fieldType = BindingFlags.Public | BindingFlags.Instance;

                foreach (Attribute attr in dsType.GetCustomAttributes(true))
                {
                    // Check for the attribute.
                    if (attr.GetType() == typeof(ExFormatterAttrAttribute))
                    {
                        checkSize = ((ExFormatterAttrAttribute)attr).CheckSize;
                        sortType = ((ExFormatterAttrAttribute)attr).SortFormatType;
                        fieldType = ((ExFormatterAttrAttribute)attr).FieldType;
                    }
                }

                FieldInfo[] fInfos = dsType.GetFields(fieldType);
//                int length = fInfos.Length;

                switch (sortType)
                {
                    case SortType.SortByName:
                        Array.Sort(fInfos, new SortByName());
                        break;
                    case SortType.SortByOrder:
                        break;
                }

                Byte[] temp;
                foreach (FieldInfo item in fInfos)
                {
                    temp = _ConvertToStreamByte(item.GetValue(mainData), item.FieldType);
                    for (int j = 0; j < temp.Length; j++)
                        data.Add(temp[j]);
                }

                if (checkSize)
                {
                    int cSize = ((ExFormatterBinary)mainData).CheckSize();
                    if (data.Count != cSize)
                        throw new Exception("Check size encounters a wrong size length. Stream size is " + data.Count + " which is not equal to " + cSize + ".");
                }
            }
            else if (dsType.Equals(typeof(ExFormatterBinary)))
            {
                bool checkSize = false;
                SortType sortType = SortType.SortByOrder;
                BindingFlags fieldType = BindingFlags.Public | BindingFlags.Instance;

                foreach (Attribute attr in mainData.GetType().GetCustomAttributes(false))
                {
                    // Check for the attribute.
                    if (attr.GetType() == typeof(ExFormatterAttrAttribute))
                    {
                        checkSize = ((ExFormatterAttrAttribute)attr).CheckSize;
                        sortType = ((ExFormatterAttrAttribute)attr).SortFormatType;
                        fieldType = ((ExFormatterAttrAttribute)attr).FieldType;
                    }
                }

                FieldInfo[] fInfos = mainData.GetType().GetFields(fieldType);
//                int length = fInfos.Length;

                switch (sortType)
                {
                    case SortType.SortByName:
                        Array.Sort(fInfos, new SortByName());
                        break;
                    case SortType.SortByOrder:
                        break;
                }

                Byte[] temp;
                foreach (FieldInfo item in fInfos)
                {
                    temp = _ConvertToStreamByte(item.GetValue(mainData), item.FieldType);
                    for (int j = 0; j < temp.Length; j++)
                        data.Add(temp[j]);
                }

                if (checkSize)
                {
                    int cSize = ((ExFormatterBinary)mainData).CheckSize();
                    if (data.Count != cSize)
                        throw new Exception("Check size encounters a wrong size length. Stream size is " + data.Count + " which is not equal to " + cSize + ".");
                }
            }
            else
            {
                Type baseType = dsType.BaseType;
//                Type lastType = dsType;

                while (baseType != null)
                {
                    if (baseType.Equals(typeof(ExFormatterBinary)))
                    {

                        Byte[] temp = null;

                        temp = ((ExFormatterBinary)mainData)._ConvertToStreamByte((ExFormatterBinary)mainData, typeof(ExFormatterBinary));

                        for (int j = 0; j < temp.Length; j++)
                            data.Add(temp[j]);
                        break;
                    }
//                    lastType = baseType;
                    baseType = baseType.BaseType;
                }

                if (baseType == null)
                {
                    if (dsType.GetInterface(typeof(IExFormatter).Name) != null && dsType.GetInterface(typeof(IExFormatter).Name).Equals(typeof(IExFormatter)))
                    {
                        Byte[] temp = null;
                        temp = ((IExFormatter)mainData).ToByteArray();

                        for (int j = 0; j < temp.Length; j++)
                            data.Add(temp[j]);
                    }
                    else
                        throw new System.Exception("Can not convert field of type " + dsType.Name + " to byte[].");
                }
            }

            return data.ToArray();
        }

        private void ByteValueConvert(object mainData, Type dsType, ref List<Byte> data)
        {
            if (dsType.Equals(typeof(SByte)))
            {
                Byte temp = 0;
                object value = null;
                value = mainData;
                temp = (Byte)(SByte)value;
                data.Add(temp);
            }
            else if (dsType.Equals(typeof(Boolean)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((Boolean)mainData);
                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);
            }
            else if (dsType.Equals(typeof(Char)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((Char)mainData);
                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);
            }
            else if (dsType.Equals(typeof(Byte)))
            {
                Byte temp = 0;
                temp = (Byte)mainData;

                data.Add(temp);
            }
            else if (dsType.Equals(typeof(Double)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((Double)mainData);

                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);

            }
            else if (dsType.Equals(typeof(Int16)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((Int16)mainData);

                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);
            }
            else if (dsType.Equals(typeof(Int32)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((Int32)mainData);

                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);
            }
            else if (dsType.Equals(typeof(Int64)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((Int64)mainData);

                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);
            }
            else if (dsType.Equals(typeof(Single)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((Single)mainData);

                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);
            }
            else if (dsType.Equals(typeof(UInt16)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((UInt16)mainData);

                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);
            }
            else if (dsType.Equals(typeof(UInt32)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((UInt32)mainData);

                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);
            }
            else if (dsType.Equals(typeof(UInt64)))
            {
                Byte[] temp = null;
                temp = BitConverter.GetBytes((UInt64)mainData);

                for (int j = 0; j < temp.Length; j++)
                    data.Add(temp[j]);
            }
        }

        private void ArrayToStreamByte(object mainData, Type dsType, ref List<Byte> data)
        {
           Byte[] temp = null;
           Array array = (Array)mainData;
           int rank = array.Rank;
           Int64[] indices = new Int64[rank];
           int[] lowerBounds = new int[rank];
           int[] upperBounds = new int[rank];

           for (int i = 0; i < rank; i++)
           {
               lowerBounds[i] = array.GetLowerBound(i);
               upperBounds[i] = array.GetUpperBound(i);
               indices[i] = lowerBounds[i];
           }

           for (int i = 0; i < array.Length; i++)
           {
               object tempObj = array.GetValue(indices);
               Type curType = tempObj.GetType();

               if (curType.BaseType.Equals(typeof(ExFormatterBinary)))
               {
                   BryFoerToByteStream(tempObj, curType, ref data);
               }
               else if (curType.IsArray)
               {
                   ArrayToStreamByte(tempObj, curType, ref data);
               }
               else
               {
                   ByteValueConvert(tempObj, curType, ref data);
                   array.SetValue(tempObj, indices);

                   _IterateIndices(ref indices, lowerBounds, upperBounds, rank - 1);
               }

               if (curType.IsArray || curType.BaseType.Equals(typeof(ExFormatterBinary)))
               {
                   _IterateIndices(ref indices, lowerBounds, upperBounds, rank - 1);
                   for (int j = 0; j < temp.Length; j++)
                       data.Add(temp[j]);
               }
           }
        }

        private void BryFoerToByteStream(object mainData, Type dsType, ref List<Byte> data)
        {
            bool checkSize = false;
            SortType sortType = SortType.SortByOrder;
            BindingFlags fieldType = BindingFlags.Public | BindingFlags.Instance;

            foreach (Attribute attr in mainData.GetType().GetCustomAttributes(false))
            {
                // Check for the attribute.
                if (attr.GetType() == typeof(ExFormatterAttrAttribute))
                {
                    checkSize = ((ExFormatterAttrAttribute)attr).CheckSize;
                    sortType = ((ExFormatterAttrAttribute)attr).SortFormatType;
                    fieldType = ((ExFormatterAttrAttribute)attr).FieldType;
                }
            }

            FieldInfo[] fInfos = mainData.GetType().GetFields(fieldType);
//            int length = fInfos.Length;

            switch (sortType)
            {
                case SortType.SortByName:
                    Array.Sort(fInfos, new SortByName());
                    break;
                case SortType.SortByOrder:
                    break;
            }

            foreach (FieldInfo item in fInfos)
            {
                Type curType = item.FieldType;
                object tempObj = item.GetValue(mainData);


                if (curType.BaseType.Equals(typeof(ExFormatterBinary)))
                {
                    BryFoerToByteStream(tempObj, curType, ref data);
                }
                else if (curType.IsArray)
                {
                    ArrayToStreamByte(tempObj, curType, ref data);
                }
                else
                {
                    ByteValueConvert(tempObj, curType, ref data);
                }
                
            }

            if (checkSize)
            {
                int cSize = ((ExFormatterBinary)mainData).CheckSize();
                if (data.Count != cSize)
                    throw new Exception("Check size encounters a wrong size length. Stream size is " + data.Count + " which is not equal to " + cSize + ".");
            }
        }

        private Byte[] newConvertToByteStream(object mainData, Type dsType)
        {
            List<Byte> data = new List<Byte>();

            if (dsType.BaseType != null)
            {
                if (dsType.BaseType.Equals(typeof(ExFormatterBinary)))
                {
                    BryFoerToByteStream(mainData, dsType, ref data);
                }
                else if (dsType.IsArray)
                {
                    ArrayToStreamByte(mainData, dsType, ref data);
                }
                else
                {

                }
            }

            return data.ToArray();
        }

        public virtual Byte[] ToByteArray()
        {
            return _ConvertToStreamByte(this, this.GetType());
            //return newConvertToByteStream(this, this.GetType());
        }

        public static int IndexByteToString(byte[] arr)
		{
			int index = (GUtil.indexOf<byte>(0, arr));
				if(index == -1) index = arr.Length;
				else index = index + 0;
			return index;
		}

        private void _FromByteStreamConvert(ref object mainData, Type dsType, Byte[] data, int startIndex, ref int size)
        {
            if (dsType.Equals(typeof(SByte)))
            {
                size = sizeof(SByte);
                if (data != null)
                {
                    mainData = (SByte)data[startIndex];
                }
            }
            else if (dsType.Equals(typeof(Boolean)))
            {
                size = sizeof(Boolean);
                if (data != null)
                {
                    mainData = BitConverter.ToBoolean(data, startIndex);
                }
            }
            else if (dsType.Equals(typeof(Char)))
            {
                size = sizeof(Char);
                if (data != null)
                {
                    mainData = BitConverter.ToChar(data, startIndex);
                }
            }
            else if (dsType.Equals(typeof(Byte)))
            {
                size = sizeof(Byte);
                if (data != null)
                {
                    mainData = data[startIndex];
                }
            }
            else if (dsType.Equals(typeof(Double)))
            {
                size = sizeof(Double);
                if (data != null)
                {
                    mainData = BitConverter.ToDouble(data, startIndex);
                }
            }
            else if (dsType.Equals(typeof(Int16)))
            {
                size = sizeof(Int16);
                if (data != null)
                {
                    mainData = BitConverter.ToInt16(data, startIndex);
                }
            }
            else if (dsType.Equals(typeof(Int32)))
            {
                size = sizeof(Int32);
                if (data != null)
                {
                    mainData = BitConverter.ToInt32(data, startIndex);
                }
            }
            else if (dsType.Equals(typeof(Int64)))
            {
                size = sizeof(Int64);
                if (data != null)
                {
                    mainData = BitConverter.ToInt64(data, startIndex);
                }
            }
            else if (dsType.Equals(typeof(Single)))
            {
                size = sizeof(Single);
                if (data != null)
                {
                    mainData = BitConverter.ToSingle(data, startIndex);
                }
            }
            else if (dsType.Equals(typeof(UInt16)))
            {
                size = sizeof(UInt16);
                if (data != null)
                {
                    mainData = BitConverter.ToUInt16(data, startIndex);
                }
            }
            else if (dsType.Equals(typeof(UInt32)))
            {
                size = sizeof(UInt32);
                if (data != null)
                {
                    mainData = BitConverter.ToUInt32(data, startIndex);
                }
            }
            else if (dsType.Equals(typeof(UInt64)))
            {
                size = sizeof(UInt64);
                if (data != null)
                {
                    mainData = BitConverter.ToUInt64(data, startIndex);
                }
            }
            else if (dsType.IsArray)
            {
                Array array = (Array)mainData;
                int rank = array.Rank;
                Int64[] indices = new Int64[rank];
                int[] lowerBounds = new int[rank];
                int[] upperBounds = new int[rank];

                for (int i = 0; i < rank; i++)
                {
                    lowerBounds[i] = array.GetLowerBound(i);
                    upperBounds[i] = array.GetUpperBound(i);
                    indices[i] = lowerBounds[i];
                }

                int s;
                int ofs = 0;
                for (int i = 0; i < array.Length; i++)
                {
                    s = 0;
                    object item = array.GetValue(indices);
                    _FromByteStreamConvert(ref item, dsType.GetElementType(), data, startIndex + ofs, ref s);
                    array.SetValue(item, indices);

                    _IterateIndices(ref indices, lowerBounds, upperBounds, rank - 1);

                    ofs += s;
                }
                size += ofs;
            }
            else if (dsType.BaseType != null && dsType.BaseType.Equals(typeof(ExFormatterBinary)))
            {
                bool checkSize = false;
                SortType sortType = SortType.SortByOrder;
//                BindingFlags fieldType = BindingFlags.Public | BindingFlags.Instance;

                foreach (Attribute attr in dsType.GetCustomAttributes(true))
                {
                    // Check for the attribute.
                    if (attr.GetType() == typeof(ExFormatterAttrAttribute))
                    {
                        checkSize = ((ExFormatterAttrAttribute)attr).CheckSize;
                        sortType = ((ExFormatterAttrAttribute)attr).SortFormatType;
//                        fieldType = ((ExFormatterAttrAttribute)attr).FieldType;
                    }
                }

                FieldInfo[] fInfos = dsType.GetFields();
//                int length = fInfos.Length;

                switch (sortType)
                {
                    case SortType.SortByName:
                        Array.Sort(fInfos, new SortByName());
                        break;
                    case SortType.SortByOrder:
                        break;
                }

                int s;
                int ofs = 0;
                foreach (FieldInfo item in fInfos)
                {
                    if (item.IsStatic)
                    {
                        continue;
                    }
                    s = 0;
                    object temp = item.GetValue(mainData);
                    _FromByteStreamConvert(ref temp, item.FieldType, data, startIndex + ofs, ref s);
                    if (data != null)
                        item.SetValue(mainData, temp);
                    ofs += s;
                }
                size += ofs;

                if (checkSize)
                {
                    int cSize = ((ExFormatterBinary)mainData).CheckSize();
                    if (size != cSize)
                        throw new Exception("Check size encounters a wrong size length. Stream size is " + size + " which is not equal to " + cSize + ".");
                }
            }
            else if (dsType.Equals(typeof(ExFormatterBinary)))
            {
                bool checkSize = false;
                SortType sortType = SortType.SortByOrder;
//                BindingFlags fieldType = BindingFlags.Public | BindingFlags.Instance;

                foreach (Attribute attr in mainData.GetType().GetCustomAttributes(false))
                {
                    // Check for the attribute.
                    if (attr.GetType() == typeof(ExFormatterAttrAttribute))
                    {
                        checkSize = ((ExFormatterAttrAttribute)attr).CheckSize;
                        sortType = ((ExFormatterAttrAttribute)attr).SortFormatType;
//                        fieldType = ((ExFormatterAttrAttribute)attr).FieldType;
                    }
                }

                FieldInfo[] fInfos = mainData.GetType().GetFields();
//                int length = fInfos.Length;

                switch (sortType)
                {
                    case SortType.SortByName:
                        Array.Sort(fInfos, new SortByName());
                        break;
                    case SortType.SortByOrder:
                        break;
                }

                int s;
                int ofs = 0;
                foreach (FieldInfo item in fInfos)
                {
                    if (item.IsStatic)
                    {
                        continue;
                    }
                    s = 0;
                    object temp = item.GetValue(mainData);
                    _FromByteStreamConvert(ref temp, item.FieldType, data, startIndex + ofs, ref s);
                    if (data != null)
                        item.SetValue(mainData, temp);
                    ofs += s;
                }
                size += ofs;

                if (checkSize)
                {
                    int cSize = ((ExFormatterBinary)mainData).CheckSize();
                    if (size != cSize)
                        throw new Exception("Check size encounters a wrong size length. Stream size is " + size + " which is not equal to " + cSize + ".");
                }
            }
            else
            {
                Type baseType = dsType.BaseType;
//                Type lastType = dsType;

                while (baseType != null)
                {
                    if (baseType.Equals(typeof(ExFormatterBinary)))
                    {
                        int s = 0;
                        _FromByteStreamConvert(ref mainData, typeof(ExFormatterBinary), data, startIndex, ref s);
                        size += s;
                        break;
                    }
//                    lastType = baseType;
                    baseType = baseType.BaseType;
                }

                if (baseType == null)
                {
                    if (dsType.GetInterface(typeof(IExFormatter).Name) != null && dsType.GetInterface(typeof(IExFormatter).Name).Equals(typeof(IExFormatter)))
                    {
                        IExFormatter registeredData = (IExFormatter)mainData;
                        size = registeredData.Size;
                        if (data != null)
                        {
                            Byte[] subArray = new Byte[data.Length - startIndex];
                            Array.Copy(data, startIndex, subArray, 0, subArray.Length);
                            registeredData.FromByteArray(subArray);
                        }
                    }
                    else
                        throw new System.Exception("Can not convert byte[] to field of type " + dsType.Name + ".");
                }
            }
        }
		
		public static int byteToStringIndex(byte[] arr)
        {
            int index = (GUtil.indexOf<byte>(0, arr));
            if (index == -1) index = arr.Length;
            else index = index + 0;
            return index;
        }
		
        private void BaseValueConvert(ref object mainData, Type dsType, Byte[] data, ref int startIndex, ref int size)
        {
            if (dsType.Equals(typeof(SByte)))
            {
                size = sizeof(SByte);
                if (data != null)
                {
                    mainData = (SByte)data[startIndex];
                }
            }
            else if (dsType.Equals(typeof(Boolean)))
            {
                size = sizeof(Boolean);
                if (data != null)
                {
                    mainData = BitConverter.ToBoolean(data, startIndex);
                }
            }
            else if (dsType.Equals(typeof(Char)))
            {
                size = sizeof(Char);
                if (data != null)
                {
                    mainData = BitConverter.ToChar(data, startIndex);
                }
            }
            else if (dsType.Equals(typeof(Byte)))
            {
                size = sizeof(Byte);
                if (data != null)
                {
                    mainData = data[startIndex];
                }
            }
            else if (dsType.Equals(typeof(Double)))
            {
                size = sizeof(Double);
                if (data != null)
                {
                    mainData = BitConverter.ToDouble(data, startIndex);
                }
            }
            else if (dsType.Equals(typeof(Int16)))
            {
                size = sizeof(Int16);
                if (data != null)
                {
                    mainData = BitConverter.ToInt16(data, startIndex);
                }
            }
            else if (dsType.Equals(typeof(Int32)))
            {
                size = sizeof(Int32);
                if (data != null)
                {
                    mainData = BitConverter.ToInt32(data, startIndex);
                }
            }
            else if (dsType.Equals(typeof(Int64)))
            {
                size = sizeof(Int64);
                if (data != null)
                {
                    mainData = BitConverter.ToInt64(data, startIndex);
                }
            }
            else if (dsType.Equals(typeof(Single)))
            {
                size = sizeof(Single);
                if (data != null)
                {
                    mainData = BitConverter.ToSingle(data, startIndex);
                }
            }
            else if (dsType.Equals(typeof(UInt16)))
            {
                size = sizeof(UInt16);
                if (data != null)
                {
                    mainData = BitConverter.ToUInt16(data, startIndex);
                }
            }
            else if (dsType.Equals(typeof(UInt32)))
            {
                size = sizeof(UInt32);
                if (data != null)
                {
                    mainData = BitConverter.ToUInt32(data, startIndex);
                }
            }
            else if (dsType.Equals(typeof(UInt64)))
            {
                size = sizeof(UInt64);
                if (data != null)
                {
                    mainData = BitConverter.ToUInt64(data, startIndex);
                }
            }
        }

        private int LengthOfTypeGet(Type dsType)
        {
            if (dsType.Equals(typeof(SByte)))
            {
                return sizeof(SByte);
            }
            else if (dsType.Equals(typeof(Boolean)))
            {
                return sizeof(Boolean);
            }
            else if (dsType.Equals(typeof(Char)))
            {
                return sizeof(Char);
            }
            else if (dsType.Equals(typeof(Byte)))
            {
                return sizeof(Byte);
            }
            else if (dsType.Equals(typeof(Double)))
            {
                return sizeof(Double);
            }
            else if (dsType.Equals(typeof(Int16)))
            {
                return sizeof(Int16);
            }
            else if (dsType.Equals(typeof(Int32)))
            {
                return sizeof(Int32);
            }
            else if (dsType.Equals(typeof(Int64)))
            {
                return sizeof(Int64);
            }
            else if (dsType.Equals(typeof(Single)))
            {
                return sizeof(Single);
            }
            else if (dsType.Equals(typeof(UInt16)))
            {
                return sizeof(UInt16);
            }
            else if (dsType.Equals(typeof(UInt32)))
            {
                return sizeof(UInt32);
            }
            else if (dsType.Equals(typeof(UInt64)))
            {
                return sizeof(UInt64);
            }

            return -1;
        }

        public void NewFromByteStreamConvert(ref object mainData, Type dsType, Byte[] data, int startIndex, ref int size)
        {
            List<Type> typeList = new List<Type>();

            if (dsType.BaseType != null)
            {
                if (dsType.BaseType.Equals(typeof(ExFormatterBinary)))
                {
                    FromFormatterConvert(ref mainData, dsType, data, ref startIndex, ref size, ref typeList );
                }
                else if (dsType.IsArray)
                {
                    FromArrayConvert(ref mainData, dsType, data, ref startIndex, ref size, ref typeList);
                }
                else
                {

                }
            }
        }

        public void FromArrayConvert(ref object mainData, Type dsType, Byte[] data, ref int startIndex, ref int size, ref List<Type> typeList)
        {
            Array array = (Array)mainData;
            int rank = array.Rank;
            Int64[] indices = new Int64[rank];
            int[] lowerBounds = new int[rank];
            int[] upperBounds = new int[rank];

            for (int i = 0; i < rank; i++)
            {
                lowerBounds[i] = array.GetLowerBound(i);
                upperBounds[i] = array.GetUpperBound(i);
                indices[i] = lowerBounds[i];
            }


            for (int i = 0; i < array.Length; i++)
            {
                object tempObj = array.GetValue(indices);
                Type curType = tempObj.GetType();

                if (curType.BaseType.Equals(typeof(ExFormatterBinary)))
                {
                    FromFormatterConvert(ref tempObj, curType, data, ref startIndex, ref size, ref typeList);
                }
                else if (curType.IsArray)
                {
                    FromArrayConvert(ref tempObj, curType, data, ref startIndex, ref size, ref typeList);
                }
                else
                {
                    BaseValueConvert(ref tempObj, curType, data, ref startIndex, ref size);
                    array.SetValue(tempObj, indices);

                    _IterateIndices(ref indices, lowerBounds, upperBounds, rank - 1);

                    startIndex += size;
                }

                if (curType.IsArray || curType.BaseType.Equals(typeof(ExFormatterBinary)))
                {
                    array.SetValue(tempObj, indices);
                    _IterateIndices(ref indices, lowerBounds, upperBounds, rank - 1);
                }
            }
        }

        public void FromFormatterConvert(ref object mainData, Type dsType, Byte[] data, ref int startIndex, ref int size, ref List<Type> typeList)
        {
            FieldInfo[] fInfos = dsType.GetFields();
            foreach (FieldInfo item in fInfos)
            {
                if (item.IsStatic)
                {
                    continue;
                }

                Type curType = item.FieldType;
                object tempObj = item.GetValue(mainData);

                if (curType.BaseType.Equals(typeof(ExFormatterBinary)))
                {
                    FromFormatterConvert(ref tempObj, curType, data, ref startIndex, ref size, ref typeList);
                }
                else if (curType.IsArray)
                {
                    FromArrayConvert(ref tempObj, curType, data, ref startIndex, ref size, ref typeList);
                }
                else
                {
                    BaseValueConvert(ref tempObj, curType, data, ref startIndex, ref size);
                    if (data != null)
                        item.SetValue(mainData, tempObj);
                    startIndex += size;
                }
            }
        }

        public virtual void FromByteArray(Byte[] data, object ProData)
        {
            int size = 0;
            object t = ProData;
            //newConvertFromByteStream(ref t, this.GetType(), data, 0, ref size);
            _FromByteStreamConvert(ref t, this.GetType(), data, 0, ref size);
        }

        public virtual void FromByteArrayNew(Byte[] data, object ProData)
        {
            int size = 0;
            object t = ProData;
            NewFromByteStreamConvert(ref t, this.GetType(), data, 0, ref size);
            //_ConvertFromByteStream(ref t, this.GetType(), data, 0, ref size);
        }

		private int _size(object mainData)
        {
            int size = 0;
            object t = this;
            _FromByteStreamConvert(ref t, this.GetType(), null, 0, ref size);
            return size;
        }
    }

    [Serializable]
    public class SortByName : IComparer
    {
        public int Compare(object item1, object item2)
        {
            return String.Compare(((FieldInfo)item1).Name, ((FieldInfo)item2).Name);
        }
    }

    [Serializable]
    public class SizedString : IExFormatter
    {
        private int __size = 0;
        private int __length = 0;
        private Char[] __str;

        public string Value
        {
            get
            {
                return new string(__str, 0, __length);
            }
            set
            {
                int len;
                if (value.Length > __size)
                    len = __size;
                else
                    len = value.Length;
                value.CopyTo(0, __str, 0, len);
                __length = len;
            }
        }

        private SizedString()
        {
        }

        public SizedString(Byte size)
        {
            __str = new Char[size];
            __size = size;
            __length = 0;
        }

        public int Length
        {
            get
            {
                return __length;
            }
        }

        public int Size
        {
            get
            {
                return __size + 1;
            }
        }

        public int Capacity
        {
            get
            {
                return __size;
            }
        }

        static public implicit operator String(SizedString str)
        {
            return str.Value;
        }

        public Byte[] ToByteArray()
        {
            Byte[] retValue = new byte[__size + 1];

            retValue[0] = (Byte)__length;
            for (int i = 0; i < __size; i++)
            {
                retValue[i + 1] = (Byte)__str[i];
            }

            return retValue;
        }

        public void FromByteArray(Byte[] data)
        {
            int stringSize = __size + 1;
            int counter = 0;

            Char[] chars = new char[stringSize];

            for (int i = 0; i < stringSize - 1; i++)
            {
                chars[i] = (Char)data[i + 1];
                if (chars[i] == (Char)0 && counter == 0)
                {
                    counter = i;
                }
            }

            Value = new string(chars, 0, counter);
        }
    }

    public interface IExFormatter
    {
        int Size
        {
            get;
        }
        Byte[] ToByteArray();
        void FromByteArray(Byte[] data);
    }


    [Serializable]
    public enum SortType
    {
        SortByName,
        SortByOrder,
    }

    [Serializable]
    public class ExFormatterAttrAttribute : Attribute
    {
        private bool __checkSize = false;

        public bool CheckSize
        {
            get
            {
                return __checkSize;
            }
            set
            {
                __checkSize = value;
            }
        }

        private SortType __sortType = SortType.SortByOrder;

        public SortType SortFormatType
        {
            get
            {
                return __sortType;
            }
            set
            {
                __sortType = value;
            }
        }

        private BindingFlags __fieldType = BindingFlags.Public | BindingFlags.Instance;

        public BindingFlags FieldType
        {
            get
            {
                return __fieldType;
            }
            set
            {
                __fieldType = value;
            }
        }

        public ExFormatterAttrAttribute(SortType sortType)
        {
            __sortType = sortType;
        }

        public ExFormatterAttrAttribute(bool checkSize)
        {
            __checkSize = checkSize;
        }

        public ExFormatterAttrAttribute(BindingFlags fieldType)
        {
            __fieldType = fieldType;
        }

        public ExFormatterAttrAttribute(SortType sortType, BindingFlags fieldType)
        {
            __sortType = sortType;
            __fieldType = fieldType;
        }

        public ExFormatterAttrAttribute(SortType sortType, BindingFlags fieldType, bool checkSize)
        {
            __sortType = sortType;
            __fieldType = fieldType;
            __checkSize = checkSize;
        }
    }
}
