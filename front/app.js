Ext.application({
    requires: [
        'Ext.container.Viewport',
        'ToB.Registry.Panel'
    ],
    
    name: 'ToB',

    appFolder: 'app',

    launch: function() {
        Ext.create('Ext.container.Viewport', {
            items: [
                Ext.create('Ext.panel.Panel', {
                    title: 'Get random position',
                    height: 100,
                    region: 'north',
                    layout: {
                        type: 'hbox'
                    },
                    items: [
                        {
                            xtype: 'button',
                            text: 'GetRandom'
                        },
                        {
                            xtype: 'textfield',
                            fieldLabel: 'Result',
                        }
                    ]
                }),
                Ext.create('ToB.Registry.Panel', {
                    layout: {
                        type: 'vbox'
                    },
                })
            ]
        });
    }
});