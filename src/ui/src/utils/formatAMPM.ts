const formatAMPM = (date: Date) => {
    let hours = date.getHours();
    let minutes = date.getMinutes();
    let ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12;
    let minutesStr = minutes < 10 ? '0' + minutes : minutes;
    let strTime = hours + ':' + minutesStr + ' ' + ampm;
    return strTime;
}

export default formatAMPM;