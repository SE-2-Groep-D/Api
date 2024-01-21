console.log("Tracking script enabled.")

// Globale variabelen
const buttons = document.querySelectorAll('button');
const links = document.querySelectorAll('a');

// Constante variabelen.
const apiURL = document.getElementById('tracking-script').src;

const domain = window.location.hostname;
const page = window.location.pathname;
const browser = getBrowserName();


let pagePercentage = 0;
let timeInSeconds = 0;
let timeTillAction = 0;
let clickedItems = [];

// Start een timer om de begin tijd te meten.
setInterval(() => {
    timeInSeconds++;
}, 1000);

function updateScrollPercentage() {
    var windowHeight = window.innerHeight;
    var documentHeight = document.documentElement.scrollHeight - windowHeight;
    var scrollTop = window.scrollY || window.pageYOffset || document.body.scrollTop + (document.documentElement && document.documentElement.scrollTop || 0);

    var scrollPercentage = (scrollTop / documentHeight) * 100;

    if (scrollPercentage <= pagePercentage) return;
    pagePercentage = (scrollPercentage > 100) ? 100 : scrollPercentage.toFixed(0);
}

async function sendData() {
    console.log("Sending tracking data to: " + apiURL);

    const data = JSON.stringify({
        domain: domain,
        page: page,
        browser: browser,
        pagePercentage: pagePercentage,
        timeInSeconds: timeInSeconds,
        timeTillAction: timeTillAction,
        clickedItems: clickedItems
    });

    console.log(data)

    const response = await fetch(apiURL, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: data,
    });


    if (response.status !== 200) return;
    console.log("Succesfully send data.")
}

function updateTimeTillAction() {
    if (timeTillAction !== 0) return;
    timeTillAction = timeInSeconds;
}

function addClick(e) {
    if (timeTillAction === 0) updateTimeTillAction();
    const target = e.target;
    const type = target.tagName;

    let click = {
        itemId: target.id,
        itemType: 'button',
        timeInSeconds: timeInSeconds,
        text: target.innerText,
    }

    if (type === 'A') {
        click.href = target.href;
        click.type = 'link';
    }

    clickedItems.push(click);
}

function getBrowserName() {
    const userAgent = navigator.userAgent;

    if (userAgent.includes('Chrome')) {
        return 'Google Chrome';
    } else if (userAgent.includes('Firefox')) {
        return 'Mozilla Firefox';
    } else if (userAgent.includes('Safari')) {
        return 'Apple Safari';
    } else if (userAgent.includes('Edge')) {
        return 'Microsoft Edge';
    } else if (navigator.userAgent.includes('MSIE') || navigator.userAgent.includes('Trident')) {
        return 'Internet Explorer';
    } else {
        return 'Unknown Browser';
    }
}

buttons.forEach(button => button.addEventListener('click', addClick));
links.forEach(button => button.addEventListener('click', addClick));
document.addEventListener('scroll', updateScrollPercentage);
window.addEventListener('beforeunload', sendData);
document.getElementById("submit-tracking-data").addEventListener('click', sendData)


