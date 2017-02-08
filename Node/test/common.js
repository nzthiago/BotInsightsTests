var assert = require('assert');

function testBot(bot, messages, done) {
  var step = 1;
  var connector = bot.connector();
  bot.on('send', function (message) {
      
    if (step <= messages.length && step++ >= 1) {
      var check = messages[step - 2];
      
      checkInMessage(message, check, assert, (err) => {

        if (err) { 
          assert(false);
          done();
        }

        proceedNextStep(check, done);
      });
      
    } else {
      assert(false);
      setTimeout(done, 10); // Enable message from connector to appear in current test suite
    }
  });

  if (messages.length && messages[0].out) {
    step = 2;
    connector.processMessage(messages[0].out)
  }

  function checkInMessage(message, check, assert, callback) {

    if (check.type) {
      assert(message.type === check.type);
    }

    if (typeof check.in === 'function') {
      return check.in(message, assert, callback);
    } else {
      if (check.in) {
        if (check.in.test ? check.in.test(message.text) : message.text === check.in) {
          assert(true);
        } else {
          console.error('<%s> does not match <%s>', message.text, check.in);
          assert(false);
        }
      }
      return callback();
    }
  }

  function proceedNextStep(check, done) {
    if (check.out) {
      connector.processMessage(check.out);
    }

    if (step - 1 == messages.length) {
      setTimeout(done, 10); // Enable message from connector to appear in current test suite
    }
  }
}

module.exports = {
  testBot
}