const express = require("express");
const app = express();
const port = 3000;

app.use(express.json());

app.get("/health", (req, res) => {
  const data = {
    uptime: process.uptime(),
    message: "Ok",
    date: new Date(),
  };

  res.json(data);
});

app.post("/map", (req, res) => {
  const data = req.body.data;
  const count = req.body.count;
  const sum = data.reduce((a, b) => a + b, 0);
  const avg = data.length > 0 ? sum / data.length : 0;
  console.log({ action: "map", data, sum, avg, count });
  res.json({ sum, avg, weight: data.length / count });
});

app.post("/reduce", (req, res) => {
  const data = req.body;
  const sum = data.reduce((a, b) => a + b.sum, 0);
  const avgSum = data.reduce((prev, curr) => prev + curr.avg * curr.weight, 0);
  const avg = data.length > 0 ? avgSum / data.length : 0;

  console.log({ action: "reduce", data, sum, avg });
  res.json({ sum, avg });
});

app.post("/", (req, res) => {
  const data = req.body;
  const sum = data.reduce((a, b) => a + b, 0);
  const avg = data.length > 0 ? sum / data.length : 0;

  console.log({ data, sum, avg });

  res.json({ avg, sum });
});

app.listen(port, () => {
  console.log(`Calculator app listening at http://localhost:${port}`);
});
