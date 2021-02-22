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
                Ext.create('ToB.Registry.Panel')
            ]
        });
    }
});