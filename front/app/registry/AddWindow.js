Ext.define('ToB.registry.AddWindow', {
    extend: 'Ext.window.Window',
    
    title: 'Add item',
    
    width: 400,
    height: 100,

    okButton: null,
    textField: null,
    
    constructor: function (config) {
        let me = this,
            items = me.getItems();
        me.callParent(arguments);

        Ext.each(items, function (item) {
            me.items.add(item);
        });
    },
    
    getItems: function () {
        let me = this;
        
        me.textField = Ext.create('Ext.form.field.Text', {
            fieldLabel: 'Label',
        });
        
        me.okButton = Ext.create('Ext.button.Button', {
            text: 'OK'
        });
        
        return [
            me.textField,
            me.okButton,
            Ext.create('Ext.button.Button', {
                text: 'Cancel',
                listeners: {
                    click: function () {
                        me.close();
                    }
                }
            })
        ] 
    }
})