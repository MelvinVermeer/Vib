const defaultFetchOptions = {
    credentials: 'same-origin',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    }
}

const defaultPostOptions = Object.assign({}, defaultFetchOptions, { method: 'post' });
const defaultGetOptions = Object.assign({}, defaultFetchOptions, { method: 'get' });

const post = (url, options) => fetch(url, Object.assign({}, defaultPostOptions, options))
const get = (url, options) => fetch(url, Object.assign({}, defaultGetOptions, options))

const postJson = (url, data, options) => post(url, Object.assign({ body: JSON.stringify(data) }, options)).then(response => response.json())
const getJson = (url, data, options) => get(url, Object.assign({ body: JSON.stringify(data) }, options)).then(response => response.json())

const getMySessions = () => getJson('/api/sessions/subscribed')
const getFullSessions = () => getJson('/api/sessions/full')

const subcribe = sessionId => postJson('/api/sessions/subscribe', { 'Id': sessionId })
const unsubcribe = sessionId => postJson('/api/sessions/unsubscribe', { 'Id': sessionId })



const subscribeToSession = id => subcribe(id)
    .then(markSessionsAsSubscribed)
    .then(getFullSessions)
    .then(markSessionsAsFull)
    .then(markSlots)

const unsubscribeToSession = id => unsubcribe(id)
    .then(markSessionsAsSubscribed)
    .then(getFullSessions)
    .then(markSessionsAsFull)
    .then(markSlots)

const markSlotAsSubscribed = slot => slot.classList.add('slot__subscribed-session')
const markSlotAsUnsubscribed = slot => slot.classList.remove('slot__subscribed-session')
const hasSubscribedSession = slot => slot.querySelector('.subscribed')

const markSlots = function () {
    const slots = Array.from(document.querySelectorAll('.slot'))

    // remove class form all slots
    slots.forEach(markSlotAsUnsubscribed)

    // add class to slots with subscribed session
    slots.filter(hasSubscribedSession).forEach(markSlotAsSubscribed)
}

const markSessionAs = function (className, sessionId) {
    const session = document.querySelector(`.session[data-session-id="${sessionId}"]`)
    if (session) {
        session.classList.add(className)
    }
}

const markSessionsAs = function (className, sessionIds) {
    // eerst alles verwijderen
    const sessions = Array.from(document.querySelectorAll('.session'))
    sessions.forEach(e => e.classList.remove(className))

    // markeer de waar id klopt
    sessionIds.forEach(sessionId => markSessionAs(className, sessionId))
}

const markSessionsAsFull = sessionIds => markSessionsAs('full', sessionIds)
const markSessionsAsSubscribed = sessionIds => markSessionsAs('subscribed', sessionIds)


const showPopupForSession = function (id) {
    showPopupElement(id)
    document.body.style.overflow = 'hidden';
    // Deze regel voorkomt overscroll op iPhone, als je op dezelfde positie wilt blijven
    // zorg voor offsetTop na het sluiten van de popup
    // document.body.style.position = 'fixed';
}

const closePopupForSession = function (id) {
    hidePopupElement(id)
    document.body.style.overflow = '';
    // Deze regel voorkomt overscroll op iPhone
    // document.body.style.position = '';
}

const hideElement = element => element.classList.remove('show');
const showElement = element => element.classList.add('show');

const getPopupElement = sessionId => document.querySelector(`.session[data-session-id="${sessionId}"] .js-modal-content`)

const hidePopupElement = sessionId => hideElement(getPopupElement(sessionId))
const showPopupElement = sessionId => showElement(getPopupElement(sessionId))

const closeButtons = Array.from(document.querySelectorAll('.js-modal-close'))
closeButtons.forEach(button => button.addEventListener('click', event => {
    event.stopPropagation()
    closePopupForSession(button.dataset.sessionId)
}))

const subscribeButtons = Array.from(document.querySelectorAll('.js-subscribe'))
subscribeButtons.forEach(button => button.addEventListener('click', event => {
    event.stopPropagation()
    subscribeToSession(button.dataset.sessionId)
    closePopupForSession(button.dataset.sessionId)
}))

const unsubscribeButtons = Array.from(document.querySelectorAll('.js-unsubscribe'))
unsubscribeButtons.forEach(button => button.addEventListener('click', event => {
    event.stopPropagation()
    unsubscribeToSession(button.dataset.sessionId)
    closePopupForSession(button.dataset.sessionId)
}))

document.addEventListener('DOMContentLoaded', () => {
    const sessions = Array.from(document.querySelectorAll('.session'))
    sessions.forEach(session => session.addEventListener('click', showPopupForSession.bind(null, session.dataset.sessionId)))

    getMySessions()
        .then(markSessionsAsSubscribed)
        .then(markSlots)
})