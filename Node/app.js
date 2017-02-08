var restify = require('restify');
var bot = require('./bot');
var builder = require('botbuilder');
require('dotenv-extended').load();

// Setup Restify Server
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
    console.log('%s listening to %s', server.name, server.url);
});

var connector = new builder.ChatConnector({
    appId: process.env.MICROSOFT_APP_ID,
    appPassword: process.env.MICROSOFT_APP_PASSWORD
});

bot.create(connector);

// Listen for messages
server.post('/api/messages', connector.listen());