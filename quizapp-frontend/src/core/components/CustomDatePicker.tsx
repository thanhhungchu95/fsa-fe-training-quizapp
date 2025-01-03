import { FieldProps } from "formik";

const CustomDatePicker: React.FC<FieldProps> = ({ field, form: { setFieldValue }, ...props }) => {
    return (
        <input type="date" {...field} {...props}
            value={field.value ? field.value.toISOString().split('T')[0] : ''}
            onChange={e => setFieldValue(field.name, e.target.value ? new Date(e.target.value) : null)} />
    );
};

export default CustomDatePicker;