export const getInvitationFormUrl = (id: string | number): string => {
    let url = INVITATION_FORM_URL;
    return url.replace(idParam, id?.toString());
};

export const INIT = '';

export enum PATHS {
    MEDICINE = 'medicine',
    PHARMACY = 'pharmacy',
    INVITATION = 'invitation',
    DEMAND = 'demand',
    PURCHASE = 'purchase',
}

export enum SEGMENTS {
    NEW = 'new',
}

const idParam = ':id'

// Admnistrator:
export const PHARMACY_FORM_URL = `${PATHS.PHARMACY}/${SEGMENTS.NEW}`;
export const INVITATION_FORM_URL = `${PATHS.INVITATION}/${idParam}`; // Editar invitación
export const ADD_INVITATION_URL = getInvitationFormUrl(SEGMENTS.NEW); // Crear invitación
// export const INVITATION_FORM_URL = `${PATHS.INVITATION}/${SEGMENTS.NEW}`; // Cómo estaba antes.
export const INVITATION_LIST_URL = PATHS.INVITATION;

// Owner:
export const DEMAND_LIST_URL = PATHS.DEMAND;

// Employee:
export const MEDICINE_FORM_URL = `${PATHS.MEDICINE}/${SEGMENTS.NEW}`;
export const MEDICINE_LIST_URL = PATHS.MEDICINE;
export const DEMAND_FORM_URL = `${PATHS.DEMAND}/${SEGMENTS.NEW}`;
export const PURCHASE_LIST_URL = PATHS.PURCHASE;

