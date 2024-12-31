import { createActionGroup, emptyProps, props } from '@ngrx/store';

export const HospitalReferralsActions = createActionGroup({
  source: 'HospitalReferrals',
  events: {
    'HospitalReferral HospitalReferralss': emptyProps(),
    'HospitalReferral HospitalReferralss Success': props<{ data: unknown }>(),
    'HospitalReferral HospitalReferralss Failure': props<{ error: unknown }>(),
  }
});
