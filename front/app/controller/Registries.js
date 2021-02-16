Ext.define('AM.controller.Registries', {
   extend: 'Ext.app.Controller',
    
   stores: [
       'Registries'
   ],
    
    models: [
        'Registry'
    ],
    
    views: [
        'randomizer.List'
    ]
});