Ext.application({
    requires: ['Ext.container.Viewport'],
    name: 'AM',

    appFolder: 'app',

    // controllers: [
    //     'Users'
    // ],

    launch: function() {
        var result = {
            xtype: 'textfield',
            fieldLabel: 'result',
            name: 'res'
        }; 
        Ext.create('Ext.container.Viewport', {
            layout: 'fit',
            items: {
                xtype: 'panel',
                items: [
                    {
                        xtype: 'textfield',
                        id: 'min',
                        fieldLabel: 'min',
                        name: 'min'
                    },
                    {
                        xtype: 'textfield',
                        id: 'max',
                        fieldLabel: 'max',
                        name: 'max'
                    },
                    {
                        xtype: 'button',
                        text: 'GetRandom',
                        handler: function () {
                            var min = Ext.getCmp('min').getValue();
                            var max = Ext.getCmp('max').getValue();

                            var request = new XMLHttpRequest();
                            request.open("GET", 'https://localhost:5001/api/randomizer?min=' + min + '&max=' + max, false);
                            request.send(null);
                            var response = request.responseText;

                            Ext.getCmp('res').setValue(response);
                        }
                    },
                    {
                        xtype: 'textfield',
                        id: 'res',
                        fieldLabel: 'result',
                        name: 'res'
                    }
                ]
            }
        });
    }
});