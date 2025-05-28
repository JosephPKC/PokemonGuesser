const { env } = require('process');

const target = "http://localhost:5093";

const PROXY_CONFIG = [
  {
    context: [
      "/api",
    ],
    target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;
