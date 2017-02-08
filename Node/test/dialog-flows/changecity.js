module.exports = [
  {
    out: "hi"
  },
  {
    in: "Welcome to the Search City bot. I'm currently configured to search for things in Seattle"
  },
  {
    in: "Before get started, please tell me your name?",
    out: "Thiago"
  },
  {
    in: (message, assert, callback) => {
      assert(message && message.text && message.text.startsWith('Welcome Thiago!'));
      callback();
    },
    out: "change city to Portland"
  },
  {
    in: "All set Thiago. From now on, all my searches will be for things in Portland."
  }
];