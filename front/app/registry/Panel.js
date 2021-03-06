﻿Ext.define('ToB.registry.Panel', {
    extend: 'Ext.panel.Panel',
    
    requires: [
        'ToB.registry.AddWindow',
        'ToB.registry.Grid'
    ],

    title: 'registry list',
    
    listeners: {
        beforerender: function () {
            let me = this;
            
            me.loadLevel(me.currentLevel);
        }
    },

    url: 'http://localhost:5010/api/randomizer',

    grid: null,
    
    parentLevel: null,
    currentLevel: 1,

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
        
        me.grid = Ext.create('ToB.registry.Grid', {
            // layout: 'fit'
        });
        me.grid.on('itemdblclick', function (sender, record) {
           me.loadLevel(record.data.id); 
        });
        
        return [
            Ext.create('Ext.panel.Panel', {                
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
                    },
                    {
                        xtype: 'button',
                        text: 'Remove',
                        listeners: {
                            click: function () {
                                me.removeItem();
                            }
                        }
                    },
                    {
                        xtype: 'button',
                        text: 'Add',
                        listeners: {
                            click: function () {
                                me.addItem();
                            }
                        }
                    },
                    {
                        xtype: 'button',
                        text: 'GetRandom',
                        listeners: {
                            click: function () {
                                me.getRandomItem();
                            }
                        }
                    },
                ]
            }),
            // me.grid
            Ext.create('Ext.panel.Panel', {
                layout: 'fit',
                height: 400,
                items: [me.grid]
            })
        ]
    },

    loadLevel : function (root) {
        let me = this;

        Ext.Ajax.request({
            url: me.url + '?root=' + root,

            success: function (response) {
                let result = JSON.parse(response.responseText),
                    exists = result.length > 0,
                    store = me.grid.getStore();

                if (exists) {
                    if (root !== me.currentLevel) {
                        me.parentLevel = me.currentLevel;
                        me.currentLevel = result[0].parent;    
                    }
                    store.removeAll();
                    for (let  i = 0; i < result.length; ++i)
                        store.add(result[i])    
                }
                
            },

            failure: me.onAjaxRequestFailure
        })
    },
    
    downLevel : function() {
        let me = this,
            selection = me.grid.getSelectionModel().selected,
            isSingle = selection.getCount() === 1;
        
        if (isSingle) {
            let item  = selection.items[0].data;
            me.loadLevel(item.id);
        }
    },
    
    upLevel : function() {
        let me = this;
        
        if (me.parentLevel && me.parentLevel > 0) {
            me.loadLevel(me.parentLevel);
        }
    },

    removeItem: function() {
        let me = this,
            selection = me.grid.getSelectionModel().selected,
            isSingle = selection.getCount() === 1;
        
        if (isSingle) {
            let item = selection.items[0].data;
            Ext.Ajax.request({
                url: me.url + '?id=' + item.id,
                method: "DELETE",
                
                success: function () {
                    me.loadLevel(me.currentLevel);
                },
                
                failure: me.onAjaxRequestFailure
            });
        }
    },
    
    onAjaxRequestFailure: function (response) {
        console.log(response.statusText);
    },
    
    addItem: function () {
        let me = this,
            wnd = Ext.create('ToB.registry.AddWindow', {
                x: 10,
                y: 10
            });
        
        wnd.okButton.on('click', function () {
            let label = wnd.textField.getValue();
            if (label !== ''){
               Ext.Ajax.request({
                   url: me.url,
                   method: "POST",
                   headers: {
                       'Content-Type': 'application/json' 
                   },
                   
                   jsonData: {
                       parent: me.currentLevel,
                       label: label
                   },
                   
                   success: function () {
                       me.loadLevel(me.currentLevel);
                   },
                   
                   failure: me.onAjaxRequestFailure
               }) 
            }
            wnd.close();
        })
        wnd.show();
    },

    getRandomItem: function () {
        let me = this;
        
        Ext.Ajax.request({
            url: me.url + '/getRandom?root=' + me.currentLevel,
            
            success: function(response) {
                alert(response.responseText);
            },
            
            
            failure: me.onAjaxRequestFailurem,
        })
    }
})