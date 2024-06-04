const express = require('express');
const path = require('path');
const app = express();
const port = 3000;

// Set Content Security Policy headers
app.use((req, res, next) => {
    res.setHeader("Content-Security-Policy", "default-src 'self'; script-src 'self' 'unsafe-inline'; style-src 'self' 'unsafe-inline'; img-src 'self' data:; font-src 'self';");
    next();
});

// Serve static files from the root directory
app.use(express.static(path.join(__dirname)));

// Serve vs and Fonts directories
app.use('/vs', express.static(path.join(__dirname, 'vs')));
app.use('/Fonts', express.static(path.join(__dirname, 'Fonts')));

// Serve index.html at the root
app.get('/', (req, res) => {
    res.sendFile(path.join(__dirname, 'index.html'));
});

// Handle missing favicon.ico
app.get('/favicon.ico', (req, res) => {
    res.status(204).end();
});

app.listen(port, () => {
    console.log(`Server is running at http://localhost:${port}`);
});