export const getInvitationFormUrl = (id: string | number | null): string => {
    let url = INVITATION_FORM_URL;
    return url.replace(idParam, id!.toString());
    // return url.replace(idParam, id?.toString());
};

export const getDemandFormUrl = (id: string | number | null): string => {
    let url = DEMAND_UPDATE_URL;
    return url.replace(idParam, id!.toString());
    // return url.replace(idParam, id?.toString());
};

export const getUserFormUrl = (name: string | number | null): string => {
    let url = USER_UPDATE_URL;
    return url.replace(idParam, name!.toString());
    // return url.replace(idParam, id?.toString());
};

export const INIT = '';

export enum PATHS {
    USER = 'user',
    MEDICINE = 'medicine',
    PHARMACY = 'pharmacy',
    INVITATION = 'invitation',
    DEMAND = 'demand',
    PURCHASE = 'purchase',
    SESSION = 'session',
}

export enum SEGMENTS {
    NEW = 'new',
}

const idParam = ':id'

// Login
export const LOGIN = `${PATHS.SESSION}`;

// Register User
export const USER_FORM_URL = `${PATHS.USER}/${SEGMENTS.NEW}`;
export const USER_UPDATE_URL = `${PATHS.USER}/${idParam}`;

// User (Purchase)
export const BUY = `${PATHS.PURCHASE}`;

// Admnistrator:
export const PHARMACY_FORM_URL = `${PATHS.PHARMACY}/${SEGMENTS.NEW}`;
export const INVITATION_FORM_URL = `${PATHS.INVITATION}/${idParam}`; // Editar invitación
export const ADD_INVITATION_URL = getInvitationFormUrl(SEGMENTS.NEW); // Crear invitación
// export const INVITATION_FORM_URL = `${PATHS.INVITATION}/${SEGMENTS.NEW}`; // Cómo estaba antes.
export const INVITATION_LIST_URL = PATHS.INVITATION;

// Owner:
export const DEMAND_LIST_URL = PATHS.DEMAND;
export const DEMAND_UPDATE_URL = `${PATHS.DEMAND}/${idParam}`; // No es un componente ni SPA nueva, solo se usa para el endpoint de actualización.

// Employee:
export const MEDICINE_FORM_URL = `${PATHS.MEDICINE}/${SEGMENTS.NEW}`;
export const MEDICINE_LIST_URL = PATHS.MEDICINE;
export const DEMAND_FORM_URL = `${PATHS.DEMAND}/${SEGMENTS.NEW}`;
export const PURCHASE_LIST_URL = PATHS.PURCHASE;

