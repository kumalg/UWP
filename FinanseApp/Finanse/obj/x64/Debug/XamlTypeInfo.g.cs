﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



namespace Finanse
{
    public partial class App : global::Windows.UI.Xaml.Markup.IXamlMetadataProvider
    {
    private global::Finanse.Finanse_XamlTypeInfo.XamlTypeInfoProvider _provider;

        /// <summary>
        /// GetXamlType(Type)
        /// </summary>
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlType(global::System.Type type)
        {
            if(_provider == null)
            {
                _provider = new global::Finanse.Finanse_XamlTypeInfo.XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByType(type);
        }

        /// <summary>
        /// GetXamlType(String)
        /// </summary>
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlType(string fullName)
        {
            if(_provider == null)
            {
                _provider = new global::Finanse.Finanse_XamlTypeInfo.XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByName(fullName);
        }

        /// <summary>
        /// GetXmlnsDefinitions()
        /// </summary>
        public global::Windows.UI.Xaml.Markup.XmlnsDefinition[] GetXmlnsDefinitions()
        {
            return new global::Windows.UI.Xaml.Markup.XmlnsDefinition[0];
        }
    }
}

namespace Finanse.Finanse_XamlTypeInfo
{
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal partial class XamlTypeInfoProvider
    {
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlTypeByType(global::System.Type type)
        {
            global::Windows.UI.Xaml.Markup.IXamlType xamlType;
            if (_xamlTypeCacheByType.TryGetValue(type, out xamlType))
            {
                return xamlType;
            }
            int typeIndex = LookupTypeIndexByType(type);
            if(typeIndex != -1)
            {
                xamlType = CreateXamlType(typeIndex);
            }
            if (xamlType != null)
            {
                _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
            }
            return xamlType;
        }

        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlTypeByName(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return null;
            }
            global::Windows.UI.Xaml.Markup.IXamlType xamlType;
            if (_xamlTypeCacheByName.TryGetValue(typeName, out xamlType))
            {
                return xamlType;
            }
            int typeIndex = LookupTypeIndexByName(typeName);
            if(typeIndex != -1)
            {
                xamlType = CreateXamlType(typeIndex);
            }
            if (xamlType != null)
            {
                _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
            }
            return xamlType;
        }

        public global::Windows.UI.Xaml.Markup.IXamlMember GetMemberByLongName(string longMemberName)
        {
            if (string.IsNullOrEmpty(longMemberName))
            {
                return null;
            }
            global::Windows.UI.Xaml.Markup.IXamlMember xamlMember;
            if (_xamlMembers.TryGetValue(longMemberName, out xamlMember))
            {
                return xamlMember;
            }
            xamlMember = CreateXamlMember(longMemberName);
            if (xamlMember != null)
            {
                _xamlMembers.Add(longMemberName, xamlMember);
            }
            return xamlMember;
        }

        global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByName = new global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<global::System.Type, global::Windows.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByType = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Windows.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlMember>
                _xamlMembers = new global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlMember>();

        string[] _typeNameTable = null;
        global::System.Type[] _typeTable = null;

        private void InitTypeTables()
        {
            _typeNameTable = new string[20];
            _typeNameTable[0] = "Finanse.Views.Kategorie";
            _typeNameTable[1] = "Windows.UI.Xaml.Controls.Page";
            _typeNameTable[2] = "Windows.UI.Xaml.Controls.UserControl";
            _typeNameTable[3] = "Finanse.MainPage";
            _typeNameTable[4] = "Finanse.NowaOperacjaContentDialog";
            _typeNameTable[5] = "Windows.UI.Xaml.Controls.ContentDialog";
            _typeNameTable[6] = "Windows.UI.Xaml.Controls.ContentControl";
            _typeNameTable[7] = "Finanse.Views.PlanowaneWydatki";
            _typeNameTable[8] = "Finanse.TabHeader";
            _typeNameTable[9] = "String";
            _typeNameTable[10] = "Finanse.Wplyw";
            _typeNameTable[11] = "Finanse.WydatekTemplate";
            _typeNameTable[12] = "Finanse.Elements.Wydatek";
            _typeNameTable[13] = "Object";
            _typeNameTable[14] = "Finanse.Views.Strona_glowna";
            _typeNameTable[15] = "Finanse.Views.Szablony";
            _typeNameTable[16] = "Finanse.Views.Ustawienia";
            _typeNameTable[17] = "Finanse.WplywTemplate";
            _typeNameTable[18] = "Finanse.Elements.Wplyw";
            _typeNameTable[19] = "Finanse.Views.ZleceniaStale";

            _typeTable = new global::System.Type[20];
            _typeTable[0] = typeof(global::Finanse.Views.Kategorie);
            _typeTable[1] = typeof(global::Windows.UI.Xaml.Controls.Page);
            _typeTable[2] = typeof(global::Windows.UI.Xaml.Controls.UserControl);
            _typeTable[3] = typeof(global::Finanse.MainPage);
            _typeTable[4] = typeof(global::Finanse.NowaOperacjaContentDialog);
            _typeTable[5] = typeof(global::Windows.UI.Xaml.Controls.ContentDialog);
            _typeTable[6] = typeof(global::Windows.UI.Xaml.Controls.ContentControl);
            _typeTable[7] = typeof(global::Finanse.Views.PlanowaneWydatki);
            _typeTable[8] = typeof(global::Finanse.TabHeader);
            _typeTable[9] = typeof(global::System.String);
            _typeTable[10] = typeof(global::Finanse.Wplyw);
            _typeTable[11] = typeof(global::Finanse.WydatekTemplate);
            _typeTable[12] = typeof(global::Finanse.Elements.Wydatek);
            _typeTable[13] = typeof(global::System.Object);
            _typeTable[14] = typeof(global::Finanse.Views.Strona_glowna);
            _typeTable[15] = typeof(global::Finanse.Views.Szablony);
            _typeTable[16] = typeof(global::Finanse.Views.Ustawienia);
            _typeTable[17] = typeof(global::Finanse.WplywTemplate);
            _typeTable[18] = typeof(global::Finanse.Elements.Wplyw);
            _typeTable[19] = typeof(global::Finanse.Views.ZleceniaStale);
        }

        private int LookupTypeIndexByName(string typeName)
        {
            if (_typeNameTable == null)
            {
                InitTypeTables();
            }
            for (int i=0; i<_typeNameTable.Length; i++)
            {
                if(0 == string.CompareOrdinal(_typeNameTable[i], typeName))
                {
                    return i;
                }
            }
            return -1;
        }

        private int LookupTypeIndexByType(global::System.Type type)
        {
            if (_typeTable == null)
            {
                InitTypeTables();
            }
            for(int i=0; i<_typeTable.Length; i++)
            {
                if(type == _typeTable[i])
                {
                    return i;
                }
            }
            return -1;
        }

        private object Activate_0_Kategorie() { return new global::Finanse.Views.Kategorie(); }
        private object Activate_3_MainPage() { return new global::Finanse.MainPage(); }
        private object Activate_7_PlanowaneWydatki() { return new global::Finanse.Views.PlanowaneWydatki(); }
        private object Activate_8_TabHeader() { return new global::Finanse.TabHeader(); }
        private object Activate_10_Wplyw() { return new global::Finanse.Wplyw(); }
        private object Activate_11_WydatekTemplate() { return new global::Finanse.WydatekTemplate(); }
        private object Activate_12_Wydatek() { return new global::Finanse.Elements.Wydatek(); }
        private object Activate_14_Strona_glowna() { return new global::Finanse.Views.Strona_glowna(); }
        private object Activate_15_Szablony() { return new global::Finanse.Views.Szablony(); }
        private object Activate_16_Ustawienia() { return new global::Finanse.Views.Ustawienia(); }
        private object Activate_17_WplywTemplate() { return new global::Finanse.WplywTemplate(); }
        private object Activate_18_Wplyw() { return new global::Finanse.Elements.Wplyw(); }
        private object Activate_19_ZleceniaStale() { return new global::Finanse.Views.ZleceniaStale(); }

        private global::Windows.UI.Xaml.Markup.IXamlType CreateXamlType(int typeIndex)
        {
            global::Finanse.Finanse_XamlTypeInfo.XamlSystemBaseType xamlType = null;
            global::Finanse.Finanse_XamlTypeInfo.XamlUserType userType;
            string typeName = _typeNameTable[typeIndex];
            global::System.Type type = _typeTable[typeIndex];

            switch (typeIndex)
            {

            case 0:   //  Finanse.Views.Kategorie
                userType = new global::Finanse.Finanse_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_0_Kategorie;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 1:   //  Windows.UI.Xaml.Controls.Page
                xamlType = new global::Finanse.Finanse_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 2:   //  Windows.UI.Xaml.Controls.UserControl
                xamlType = new global::Finanse.Finanse_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 3:   //  Finanse.MainPage
                userType = new global::Finanse.Finanse_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_3_MainPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 4:   //  Finanse.NowaOperacjaContentDialog
                userType = new global::Finanse.Finanse_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.ContentDialog"));
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 5:   //  Windows.UI.Xaml.Controls.ContentDialog
                xamlType = new global::Finanse.Finanse_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 6:   //  Windows.UI.Xaml.Controls.ContentControl
                xamlType = new global::Finanse.Finanse_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 7:   //  Finanse.Views.PlanowaneWydatki
                userType = new global::Finanse.Finanse_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_7_PlanowaneWydatki;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 8:   //  Finanse.TabHeader
                userType = new global::Finanse.Finanse_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
                userType.Activator = Activate_8_TabHeader;
                userType.AddMemberName("Label");
                userType.AddMemberName("Glyph");
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 9:   //  String
                xamlType = new global::Finanse.Finanse_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 10:   //  Finanse.Wplyw
                userType = new global::Finanse.Finanse_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
                userType.Activator = Activate_10_Wplyw;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 11:   //  Finanse.WydatekTemplate
                userType = new global::Finanse.Finanse_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
                userType.Activator = Activate_11_WydatekTemplate;
                userType.AddMemberName("Wydatek");
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 12:   //  Finanse.Elements.Wydatek
                userType = new global::Finanse.Finanse_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                userType.SetIsReturnTypeStub();
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 13:   //  Object
                xamlType = new global::Finanse.Finanse_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 14:   //  Finanse.Views.Strona_glowna
                userType = new global::Finanse.Finanse_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_14_Strona_glowna;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 15:   //  Finanse.Views.Szablony
                userType = new global::Finanse.Finanse_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_15_Szablony;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 16:   //  Finanse.Views.Ustawienia
                userType = new global::Finanse.Finanse_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_16_Ustawienia;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 17:   //  Finanse.WplywTemplate
                userType = new global::Finanse.Finanse_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
                userType.Activator = Activate_17_WplywTemplate;
                userType.AddMemberName("Wplyw");
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 18:   //  Finanse.Elements.Wplyw
                userType = new global::Finanse.Finanse_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                userType.SetIsReturnTypeStub();
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 19:   //  Finanse.Views.ZleceniaStale
                userType = new global::Finanse.Finanse_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_19_ZleceniaStale;
                userType.SetIsLocalType();
                xamlType = userType;
                break;
            }
            return xamlType;
        }


        private object get_0_TabHeader_Label(object instance)
        {
            var that = (global::Finanse.TabHeader)instance;
            return that.Label;
        }
        private void set_0_TabHeader_Label(object instance, object Value)
        {
            var that = (global::Finanse.TabHeader)instance;
            that.Label = (global::System.String)Value;
        }
        private object get_1_TabHeader_Glyph(object instance)
        {
            var that = (global::Finanse.TabHeader)instance;
            return that.Glyph;
        }
        private void set_1_TabHeader_Glyph(object instance, object Value)
        {
            var that = (global::Finanse.TabHeader)instance;
            that.Glyph = (global::System.String)Value;
        }
        private object get_2_WydatekTemplate_Wydatek(object instance)
        {
            var that = (global::Finanse.WydatekTemplate)instance;
            return that.Wydatek;
        }
        private object get_3_WplywTemplate_Wplyw(object instance)
        {
            var that = (global::Finanse.WplywTemplate)instance;
            return that.Wplyw;
        }

        private global::Windows.UI.Xaml.Markup.IXamlMember CreateXamlMember(string longMemberName)
        {
            global::Finanse.Finanse_XamlTypeInfo.XamlMember xamlMember = null;
            global::Finanse.Finanse_XamlTypeInfo.XamlUserType userType;

            switch (longMemberName)
            {
            case "Finanse.TabHeader.Label":
                userType = (global::Finanse.Finanse_XamlTypeInfo.XamlUserType)GetXamlTypeByName("Finanse.TabHeader");
                xamlMember = new global::Finanse.Finanse_XamlTypeInfo.XamlMember(this, "Label", "String");
                xamlMember.SetIsDependencyProperty();
                xamlMember.Getter = get_0_TabHeader_Label;
                xamlMember.Setter = set_0_TabHeader_Label;
                break;
            case "Finanse.TabHeader.Glyph":
                userType = (global::Finanse.Finanse_XamlTypeInfo.XamlUserType)GetXamlTypeByName("Finanse.TabHeader");
                xamlMember = new global::Finanse.Finanse_XamlTypeInfo.XamlMember(this, "Glyph", "String");
                xamlMember.SetIsDependencyProperty();
                xamlMember.Getter = get_1_TabHeader_Glyph;
                xamlMember.Setter = set_1_TabHeader_Glyph;
                break;
            case "Finanse.WydatekTemplate.Wydatek":
                userType = (global::Finanse.Finanse_XamlTypeInfo.XamlUserType)GetXamlTypeByName("Finanse.WydatekTemplate");
                xamlMember = new global::Finanse.Finanse_XamlTypeInfo.XamlMember(this, "Wydatek", "Finanse.Elements.Wydatek");
                xamlMember.Getter = get_2_WydatekTemplate_Wydatek;
                xamlMember.SetIsReadOnly();
                break;
            case "Finanse.WplywTemplate.Wplyw":
                userType = (global::Finanse.Finanse_XamlTypeInfo.XamlUserType)GetXamlTypeByName("Finanse.WplywTemplate");
                xamlMember = new global::Finanse.Finanse_XamlTypeInfo.XamlMember(this, "Wplyw", "Finanse.Elements.Wplyw");
                xamlMember.Getter = get_3_WplywTemplate_Wplyw;
                xamlMember.SetIsReadOnly();
                break;
            }
            return xamlMember;
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlSystemBaseType : global::Windows.UI.Xaml.Markup.IXamlType
    {
        string _fullName;
        global::System.Type _underlyingType;

        public XamlSystemBaseType(string fullName, global::System.Type underlyingType)
        {
            _fullName = fullName;
            _underlyingType = underlyingType;
        }

        public string FullName { get { return _fullName; } }

        public global::System.Type UnderlyingType
        {
            get
            {
                return _underlyingType;
            }
        }

        virtual public global::Windows.UI.Xaml.Markup.IXamlType BaseType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlMember ContentProperty { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlMember GetMember(string name) { throw new global::System.NotImplementedException(); }
        virtual public bool IsArray { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsCollection { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsConstructible { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsDictionary { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsMarkupExtension { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsBindable { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsReturnTypeStub { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsLocalType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlType ItemType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlType KeyType { get { throw new global::System.NotImplementedException(); } }
        virtual public object ActivateInstance() { throw new global::System.NotImplementedException(); }
        virtual public void AddToMap(object instance, object key, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void AddToVector(object instance, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void RunInitializer()   { throw new global::System.NotImplementedException(); }
        virtual public object CreateFromString(string input)   { throw new global::System.NotImplementedException(); }
    }
    
    internal delegate object Activator();
    internal delegate void AddToCollection(object instance, object item);
    internal delegate void AddToDictionary(object instance, object key, object item);

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlUserType : global::Finanse.Finanse_XamlTypeInfo.XamlSystemBaseType
    {
        global::Finanse.Finanse_XamlTypeInfo.XamlTypeInfoProvider _provider;
        global::Windows.UI.Xaml.Markup.IXamlType _baseType;
        bool _isArray;
        bool _isMarkupExtension;
        bool _isBindable;
        bool _isReturnTypeStub;
        bool _isLocalType;

        string _contentPropertyName;
        string _itemTypeName;
        string _keyTypeName;
        global::System.Collections.Generic.Dictionary<string, string> _memberNames;
        global::System.Collections.Generic.Dictionary<string, object> _enumValues;

        public XamlUserType(global::Finanse.Finanse_XamlTypeInfo.XamlTypeInfoProvider provider, string fullName, global::System.Type fullType, global::Windows.UI.Xaml.Markup.IXamlType baseType)
            :base(fullName, fullType)
        {
            _provider = provider;
            _baseType = baseType;
        }

        // --- Interface methods ----

        override public global::Windows.UI.Xaml.Markup.IXamlType BaseType { get { return _baseType; } }
        override public bool IsArray { get { return _isArray; } }
        override public bool IsCollection { get { return (CollectionAdd != null); } }
        override public bool IsConstructible { get { return (Activator != null); } }
        override public bool IsDictionary { get { return (DictionaryAdd != null); } }
        override public bool IsMarkupExtension { get { return _isMarkupExtension; } }
        override public bool IsBindable { get { return _isBindable; } }
        override public bool IsReturnTypeStub { get { return _isReturnTypeStub; } }
        override public bool IsLocalType { get { return _isLocalType; } }

        override public global::Windows.UI.Xaml.Markup.IXamlMember ContentProperty
        {
            get { return _provider.GetMemberByLongName(_contentPropertyName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlType ItemType
        {
            get { return _provider.GetXamlTypeByName(_itemTypeName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlType KeyType
        {
            get { return _provider.GetXamlTypeByName(_keyTypeName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlMember GetMember(string name)
        {
            if (_memberNames == null)
            {
                return null;
            }
            string longName;
            if (_memberNames.TryGetValue(name, out longName))
            {
                return _provider.GetMemberByLongName(longName);
            }
            return null;
        }

        override public object ActivateInstance()
        {
            return Activator(); 
        }

        override public void AddToMap(object instance, object key, object item) 
        {
            DictionaryAdd(instance, key, item);
        }

        override public void AddToVector(object instance, object item)
        {
            CollectionAdd(instance, item);
        }

        override public void RunInitializer() 
        {
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(UnderlyingType.TypeHandle);
        }

        override public object CreateFromString(string input)
        {
            if (_enumValues != null)
            {
                int value = 0;

                string[] valueParts = input.Split(',');

                foreach (string valuePart in valueParts) 
                {
                    object partValue;
                    int enumFieldValue = 0;
                    try
                    {
                        if (_enumValues.TryGetValue(valuePart.Trim(), out partValue))
                        {
                            enumFieldValue = global::System.Convert.ToInt32(partValue);
                        }
                        else
                        {
                            try
                            {
                                enumFieldValue = global::System.Convert.ToInt32(valuePart.Trim());
                            }
                            catch( global::System.FormatException )
                            {
                                foreach( string key in _enumValues.Keys )
                                {
                                    if( string.Compare(valuePart.Trim(), key, global::System.StringComparison.OrdinalIgnoreCase) == 0 )
                                    {
                                        if( _enumValues.TryGetValue(key.Trim(), out partValue) )
                                        {
                                            enumFieldValue = global::System.Convert.ToInt32(partValue);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        value |= enumFieldValue; 
                    }
                    catch( global::System.FormatException )
                    {
                        throw new global::System.ArgumentException(input, FullName);
                    }
                }

                return value; 
            }
            throw new global::System.ArgumentException(input, FullName);
        }

        // --- End of Interface methods

        public Activator Activator { get; set; }
        public AddToCollection CollectionAdd { get; set; }
        public AddToDictionary DictionaryAdd { get; set; }

        public void SetContentPropertyName(string contentPropertyName)
        {
            _contentPropertyName = contentPropertyName;
        }

        public void SetIsArray()
        {
            _isArray = true; 
        }

        public void SetIsMarkupExtension()
        {
            _isMarkupExtension = true;
        }

        public void SetIsBindable()
        {
            _isBindable = true;
        }

        public void SetIsReturnTypeStub()
        {
            _isReturnTypeStub = true;
        }

        public void SetIsLocalType()
        {
            _isLocalType = true;
        }

        public void SetItemTypeName(string itemTypeName)
        {
            _itemTypeName = itemTypeName;
        }

        public void SetKeyTypeName(string keyTypeName)
        {
            _keyTypeName = keyTypeName;
        }

        public void AddMemberName(string shortName)
        {
            if(_memberNames == null)
            {
                _memberNames =  new global::System.Collections.Generic.Dictionary<string,string>();
            }
            _memberNames.Add(shortName, FullName + "." + shortName);
        }

        public void AddEnumValue(string name, object value)
        {
            if (_enumValues == null)
            {
                _enumValues = new global::System.Collections.Generic.Dictionary<string, object>();
            }
            _enumValues.Add(name, value);
        }
    }

    internal delegate object Getter(object instance);
    internal delegate void Setter(object instance, object value);

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlMember : global::Windows.UI.Xaml.Markup.IXamlMember
    {
        global::Finanse.Finanse_XamlTypeInfo.XamlTypeInfoProvider _provider;
        string _name;
        bool _isAttachable;
        bool _isDependencyProperty;
        bool _isReadOnly;

        string _typeName;
        string _targetTypeName;

        public XamlMember(global::Finanse.Finanse_XamlTypeInfo.XamlTypeInfoProvider provider, string name, string typeName)
        {
            _name = name;
            _typeName = typeName;
            _provider = provider;
        }

        public string Name { get { return _name; } }

        public global::Windows.UI.Xaml.Markup.IXamlType Type
        {
            get { return _provider.GetXamlTypeByName(_typeName); }
        }

        public void SetTargetTypeName(string targetTypeName)
        {
            _targetTypeName = targetTypeName;
        }
        public global::Windows.UI.Xaml.Markup.IXamlType TargetType
        {
            get { return _provider.GetXamlTypeByName(_targetTypeName); }
        }

        public void SetIsAttachable() { _isAttachable = true; }
        public bool IsAttachable { get { return _isAttachable; } }

        public void SetIsDependencyProperty() { _isDependencyProperty = true; }
        public bool IsDependencyProperty { get { return _isDependencyProperty; } }

        public void SetIsReadOnly() { _isReadOnly = true; }
        public bool IsReadOnly { get { return _isReadOnly; } }

        public Getter Getter { get; set; }
        public object GetValue(object instance)
        {
            if (Getter != null)
                return Getter(instance);
            else
                throw new global::System.InvalidOperationException("GetValue");
        }

        public Setter Setter { get; set; }
        public void SetValue(object instance, object value)
        {
            if (Setter != null)
                Setter(instance, value);
            else
                throw new global::System.InvalidOperationException("SetValue");
        }
    }
}

