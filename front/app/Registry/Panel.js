Ext.define('ToB.Registry.Panel', {
    extend: 'Ext.panel.Panel',
    
    requires: ['ToB.Registry.Grid'],

    title: 'Registry list',

    listeners: {
        beforerender: function () {
            let me = this;
            
            me.loadLevel(me.parentLevel);
        }
    },
    
    grid: null,
    parentLevel: 1,
    
    constructor: function (config) {
        let me = this,
            items = me.getItems();
        me.callParent(arguments);
        
        Ext.each(items, function (item) {
            me.items.add(item);
        });
    },
    
    getItems: function() {
        let me = this;
        
        me.grid = Ext.create('ToB.Registry.Grid');
        
        return [
            Ext.create('Ext.panel.Panel', {
                layout: {
                    type: 'hbox'
                },
                items: [
                    {
                        xtype: 'button',
                        text: 'Open',
                        listeners: {
                            click: function () {
                                me.downLevel();
                            }
                        }
                    },
                    {
                        xtype: 'button',
                        text: 'Back',
                        listeners: {
                            click: function () {
                                me.upLevel();
                            }
                        }
                    }
                ]
            }),
            Ext.create('Ext.panel.Panel', {
                layout: 'fit',
                height: 500,
                items: [me.grid]
            })
        ]
    },

    loadLevel : function (root) {
        let me = this;

        Ext.Ajax.request({
            url: 'http://localhost:5010/api/randomizer?root=' + root,

            success: function (response, options) {
                let result = JSON.parse(response.responseText),
                    exists = result.length > 0,
                    store = me.grid.getStore();

                if (exists) {
                    store.removeAll();
                    
                    for (let  i = 0; i < result.length; ++i)
                        store.add(result[i])    
                }
                
            },

            failure: function (response, options) {
                console.log(response.statusText);
            }
        })
    },
    
    downLevel : function() {
        let me = this,
            selection = me.grid.getSelectionModel().selected,
            isSingle = selection.getCount() === 1;
        
        if (isSingle) {
            let item  = selection.items[0].data;
            me.parentLevel = item.parent;
            me.loadLevel(item.id);
        }
    },
    
    upLevel : function() {
        let me = this;
        
        if (me.parentLevel > 0) {
            me.loadLevel(me.parentLevel);
        }
    }
})