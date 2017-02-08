var builder = require('botbuilder');

var common = require('./common');
var testBot = require('../bot');
var currentcityMessages = require('./dialog-flows/currentcity');
var changecityMessages = require('./dialog-flows/changecity');
var greetingMessages = require('./dialog-flows/greeting');

//Our parent block
describe('Bot Tests', () => {

  it('current city', function (done) { 
      var connector = new builder.ConsoleConnector();
      var bot = testBot.create(connector);

      common.testBot(bot, currentcityMessages, done);
  });

  it('change city', function (done) { 
      var connector = new builder.ConsoleConnector();

      var bot = testBot.create(connector);       
      common.testBot(bot, changecityMessages, done);
  });

    it('greeting', function (done) { 
      var connector = new builder.ConsoleConnector();

      var bot = testBot.create(connector);       
      common.testBot(bot, greetingMessages, done);
  });

});