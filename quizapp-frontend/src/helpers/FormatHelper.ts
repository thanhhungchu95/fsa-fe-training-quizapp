import moment from "moment";
import { Duration } from "moment";

const durationConversion = (duration: Duration): string => {
    let values: string[] = [];
    if (duration.days() > 0) values.push(`${duration.days()}d`);
    if (duration.hours() > 0) values.push(`${duration.hours()}h`);
    if (duration.minutes() > 0) values.push(`${duration.minutes()}m`);
    return values.join('');
}

const durationConversionFromSeconds = (seconds: number): string => {
    return durationConversion(moment.duration(seconds, 'seconds'));
}

const monthYearConversion = (): string => {
    return moment().format('MMM YYYY');
}

const currentDateConversion = (): string => {
    return moment().format('ddd, MMM Do, YYYY');
}

export const FormatHelper = {
    durationConversion,
    durationConversionFromSeconds,
    monthYearConversion,
    currentDateConversion,
};