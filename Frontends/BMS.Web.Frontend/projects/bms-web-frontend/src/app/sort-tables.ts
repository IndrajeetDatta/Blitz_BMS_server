import { BMSWebApiClientModule } from "projects/bms-web-api-client/src/public-api"

type arrayType = BMSWebApiClientModule.ChargePoint | BMSWebApiClientModule.Rfid | BMSWebApiClientModule.OcppStatus;

const compare = (v1: string | number | Date | any, v2: string | number | Date | any) => {
    if (typeof v1 === undefined) return -1
    else if (typeof v2 === undefined) return 1;
    
    if (v1 instanceof Date)
        return v1.getTime() > v2.getTime() ? 1 : v1.getTime() < v2.getTime() ? -1 : 0;
    
    return v1 < v2 ? -1 : v1 > v2 ? 1 : 0;
}

function existField(value: string, array: arrayType): value is keyof arrayType {
    return value in array;
}

export const sortArray = (array: arrayType[], directionSorting: directionSort, fieldSorting: string): arrayType[] => {
    if (directionSorting === '') return array;

    array.sort((a,b) => {
        if (existField(fieldSorting, a) && existField(fieldSorting, b)) {
            const res = compare(a[fieldSorting], b[fieldSorting]);
            return directionSorting === 'asc' ? res :  -res;
        }
        
        return 0
    })
    return array
}

export type directionSort = 'asc' | 'desc' | ''